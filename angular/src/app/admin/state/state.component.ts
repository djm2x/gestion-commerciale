import { AddAvanceComponent } from './add-avance/add-avance.component';
import { Achat } from './../../myModels/models';
import { FormControl } from '@angular/forms';
import { Component, OnInit, ViewChild, EventEmitter, Input } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatAutocompleteSelectedEvent } from '@angular/material';
import { merge, Observable } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { DeleteService } from 'src/app/layouts/delete/delete.service';
import { Fourniture, Article, Client, Fournisseur } from 'src/app/myModels/models';
import { switchMap } from 'rxjs/operators';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-state',
  templateUrl: './state.component.html',
  styleUrls: ['./state.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class StateComponent implements OnInit {
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
    { columnDef: 'fournisseur', headName: '' },
    { columnDef: 'montant', headName: '' },
    { columnDef: 'modePayement', headName: 'MODE PAYEMENT' },
    { columnDef: 'credit', headName: '' },
    { columnDef: 'avance', headName: '' },
    { columnDef: 'option', headName: 'OPTION' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName;
    return e;
  });

  fournisseurs = this.uow.fournisseurs.get();
  idFournisseur = new FormControl(0);
  idCategorie = new FormControl(0);
  d1 = new FormControl(new Date());
  displayedColumns = this.columnDefs.map(e => e.columnDef);

  credit = 0;
  avance = 0;

  myAuto = new FormControl('');
  filteredOptions: Observable<any>;
  categories = this.uow.categories.get();

  expandedElement = new Achat();

  constructor(private uow: UowService, public dialog: MatDialog, private mydialog: DeleteService, ) { }

  ngOnInit() {
    this.getPage(new Params());
    this.getCreditByFournisseur(0);
    merge(...[this.sort.sortChange, this.paginator.page, this.update]).subscribe(
      r => {
        r === true ? this.paginator.pageIndex = 0 : r = r;
        !this.paginator.pageSize ? this.paginator.pageSize = 25 : r = r;
        const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.isLoadingResults = true;
        this.getPage(
          {
            startIndex: startIndex,
            pageSize: this.paginator.pageSize,
            sortBy: this.sort.active ? this.sort.active : 'id',
            sortDir: this.sort.direction ? this.sort.direction : 'desc',
            id: this.idFournisseur.value !== 0 ? this.idFournisseur.value : 0,
            idCategorie: this.idCategorie.value !== 0 ? this.idCategorie.value : 0,
            date: this.uow.valideDate(this.d1.value),
          }
        );
      }
    );
    //
    this.autoComplete();
    //
    this.idFournisseur.valueChanges.subscribe(() => {
      this.getCreditByFournisseur(this.idFournisseur.value !== 0 ? this.idFournisseur.value : 0);
    });
  }


  addAvance() {
    const opts = {
      width: '750px',
      disableClose: true,
      data: { /*model: o,*/ title: 'Ajouter avance' }
    };

    this.dialog.open(AddAvanceComponent, opts).afterClosed().subscribe(r => {
      if (r) {
        this.update.next(true);
      }
    });
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

  getPage(data: Params) {
    this.uow.achats.getAll(data).subscribe(
      (r: any) => {
        console.log(r.list);
        this.dataSource = r.list;
        this.resultsLength = r.count;
        this.isLoadingResults = false;
      }
    );
  }

  async expandeRow(row: Achat) {

    if (this.expandedElement.id !== row.id) {
      row = await this.uow.achats.getInfoAchat(row.id).toPromise() as Achat;
      console.log(row);
    }

    return this.expandedElement = this.expandedElement.id === row.id ? new Achat() : row;
  }

  autoComplete() {
    this.filteredOptions = this.myAuto.valueChanges.pipe(
      // startWith(''),
      switchMap((value: string) => value.length > 1 ? this.uow.fournisseurs.autocomplete('societe', value) : []),
      // map(r => r)
    );
  }

  selectChange() {
    // console.lo
    this.update.next(true);
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    const o = event.option.value as Fournisseur;
    console.log(o);
    this.idFournisseur.setValue(o.id);
    this.myAuto.setValue(o.societe);
    this.update.next(true);
    // this.idOrganismeEmetteur.setValue(o.id);
  }

  getCreditByFournisseur(id) {
    this.uow.achats.getCreditByFournisseur(id).subscribe((r: { credit: number, avance: number }) => { 
      console.log(r);
      this.credit = r.credit;
      this.avance = r.avance;
    });
  }

  async delete(o: Achat) {
    const r = await this.mydialog.openDialog('Detail achat').toPromise();
    if (r === 'ok') {
      console.log(o)
      // o.fournitures.forEach(async e => {
      //   await this.uow.articles.updateQte(e.idArticle, +e.qtePrise).toPromise();
      // });
      this.uow.achats.delete(o.id).subscribe(() => {
        this.update.next(true);
      });
    }
  }

}

export class Params {
  startIndex = 0;
  pageSize = 25;
  sortBy = 'date';
  sortDir = 'desc';
  id = 0;
  idCategorie = 0;
  date = new Date();
}




