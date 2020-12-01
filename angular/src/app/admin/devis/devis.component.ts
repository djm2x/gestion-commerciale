import { SellComponent } from './sell/sell.component';
import { Devis } from './../../myModels/models';
import { FormControl } from '@angular/forms';
import { Component, OnInit, ViewChild, EventEmitter, Input } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatAutocompleteSelectedEvent } from '@angular/material';
import { merge, Observable } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { DeleteService } from 'src/app/layouts/delete/delete.service';
import { Fourniture, Article, Client, Commande, DetailCmd } from 'src/app/myModels/models';
import { switchMap } from 'rxjs/operators';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { PdfService } from '../pdf.service';

@Component({
  selector: 'app-devis',
  templateUrl: './devis.component.html',
  styleUrls: ['./devis.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class DevisComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @Input() o: Article;
  update = new EventEmitter();
  isLoadingResults = true;
  resultsLength = 0;
  isRateLimitReached = false;
  panelOpenState = false;
  dataSource = [];
  columnDefs = [
    { columnDef: 'date', headName: '' },
    { columnDef: 'client', headName: '' },
    { columnDef: 'montant', headName: '' },
    { columnDef: 'option', headName: 'OPTION' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName;
    return e;
  });

  // fournisseurs = this.uow.fournisseurs.get();
  idFournisseur = new FormControl(0);
  d1 = new FormControl(new Date());
  displayedColumns = this.columnDefs.map(e => e.columnDef);

  credit = 0;
  avance = 0;

  myAuto = new FormControl('');
  filteredOptions: Observable<any>;

  expandedElement = new Devis();

  constructor(private uow: UowService, public dialog: MatDialog
    , private mydialog: DeleteService, public pdf: PdfService) { }

  ngOnInit() {
    this.getPage(0, 10, 'id', 'desc');
    // this.getCreditByFournisseur(0);
    merge(...[this.sort.sortChange, this.paginator.page, this.update]).subscribe(
      r => {
        r === true ? this.paginator.pageIndex = 0 : r = r;
        !this.paginator.pageSize ? this.paginator.pageSize = 10 : r = r;
        const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.isLoadingResults = true;
        this.getPage(
          startIndex,
          this.paginator.pageSize,
          this.sort.active ? this.sort.active : 'id',
          this.sort.direction ? this.sort.direction : 'desc',
        );
      });

    //
    this.autoComplete();
    //
    // this.idFournisseur.valueChanges.subscribe(() => {
    //   this.getCreditByFournisseur(this.idFournisseur.value !== 0 ? this.idFournisseur.value : 0);
    // });
  }

  // all() {
  //   this.d1.setValue(null);
  //   this.idFournisseur.setValue(0);
  //   this.update.next(true);
  // }

  // reset() {
  //   this.d1.setValue(new Date());
  //   this.idFournisseur.setValue(0);
  //   this.update.next(true);
  // }
  reset() {
    this.idFournisseur.setValue(0);
    this.update.next(true);
    this.myAuto.setValue('');
  }

  getPage(startIndex, pageSize, sortBy, sortDir) {
    this.uow.deviss.getList(startIndex, pageSize, sortBy, sortDir).subscribe(r => {
      console.log(r.list);
      this.dataSource = r.list;
      this.resultsLength = r.count;
      this.isLoadingResults = false;
    });
  }

  async getPdf(o: Devis, format) {
    const d = await this.uow.deviss.getInfoDevis(o.id).toPromise() as Devis;

    const cm = new Commande();
    cm.total = d.montant;
    cm.nomClient = d.client;
    cm.client.nom = d.client;
    cm.date = d.date;
    cm.detailCmds = d.devisActicles.map(e => {
      return {
        article: e.article,
        qtePrise: e.qte,
        prixVente: e.pu,
        remise: e.remise,
        total: e.total,
      } as any;
    });

    this.pdf.generatePdf(cm, format, o.client, true);
  }

  async expandeRow(row: Devis) {

    if (this.expandedElement.id !== row.id) {
      row = await this.uow.deviss.getInfoDevis(row.id).toPromise() as Devis;
    }
    const e = this.expandedElement = this.expandedElement.id === row.id ? new Devis() : row;
    console.log(e)
    return e;
  }

  autoComplete() {
    this.filteredOptions = this.myAuto.valueChanges.pipe(
      // startWith(''),
      switchMap((value: string) => value.length > 1 ? this.uow.fournisseurs.autocomplete('nom', value) : []),
      // map(r => r)
    );
  }

  selectChange(idFournisseur) {
    // console.lo
    // this.update.next(true);
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    const o = event.option.value as Client;
    console.log(o);
    this.idFournisseur.setValue(o.id);
    this.myAuto.setValue(o.nom);
    this.update.next(true);
    // this.idOrganismeEmetteur.setValue(o.id);
  }

  getCreditByFournisseur(id) {
    this.uow.deviss.getCreditByFournisseur(id).subscribe((r: { credit: number, avance: number }) => {
      console.log(r);
      this.credit = r.credit;
      this.avance = r.avance;
    }
    );
  }

  sell(o: Devis) {
    this.dialog.open(SellComponent, {
      width: '750px',
      disableClose: true,
      data: { model: '' }
    }).afterClosed().subscribe(async r => {
      if (r) {

        o = await this.uow.deviss.getInfoDevis(o.id).toPromise() as Devis;
        const cm = new Commande();
        delete cm.client;

        cm.nomClient = o.client;
        cm.avance = o.montant;
        cm.total = o.montant;
        cm.idClient = null;

        console.log(o)

        const ncm = await this.uow.commandes.post(cm).toPromise();

        const detaiCmds: any[] = o.devisActicles.map(e => {
          return {
            idArticle: e.idArticle,
            idCommande: ncm.id,
            prixVente: e.pu,
            qtePrise: e.qte,
            remise: e.remise,
            total: e.total
          };
        });

        console.log(o.devisActicles)
        console.log(detaiCmds)

        await this.uow.detailCmds.postRange(detaiCmds as any[]).toPromise();

        const ars = o.devisActicles.map(e => {
          e.article.qteStock -= e.qte;
          // console.warn(e.article);
          return e.article;
        });

        // console.log(ars);

        await this.uow.articles.updateRange(ars).toPromise();
      }
    });
  }
}

export class Params {
  startIndex = 0;
  pageSize = 25;
  sortBy = 'date';
  sortDir = 'desc';
  id = 0;
  date = new Date();
}




