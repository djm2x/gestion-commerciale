import { PreviewComponent } from './preview/preview.component';
import { Achat } from './../../myModels/models';
import { FormControl } from '@angular/forms';
import { Component, OnInit, ViewChild, EventEmitter, Input } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatAutocompleteSelectedEvent } from '@angular/material';
import { merge, Observable } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { DeleteService } from 'src/app/layouts/delete/delete.service';
import { Fourniture, Article, Client } from 'src/app/myModels/models';
import { switchMap } from 'rxjs/operators';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-state-stock',
  templateUrl: './state-stock.component.html',
  styleUrls: ['./state-stock.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class StateStockComponent implements OnInit {
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
    { columnDef: 'categorie', headName: '' },
    { columnDef: 'code', headName: '' },
    { columnDef: 'titreFr', headName: 'article' },
    // { columnDef: 'titreAr', headName: 'AR' },
    { columnDef: 'qteStock', headName: 'QTE STOCK' },
    { columnDef: 'stockMin', headName: 'STOCK MIN' },
    // { columnDef: 'deference', headName: '' },
    { columnDef: 'option', headName: '' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName.toUpperCase();
    return e;
  });

  fournisseurs = this.uow.fournisseurs.get();
  idFilter = new FormControl(0);
  idCategorie = new FormControl(0);
  d1 = new FormControl(new Date());
  displayedColumns = this.columnDefs.map(e => e.columnDef);

  credit = 0;
  categories = this.uow.categories.get();
  myAuto = new FormControl('');
  filteredOptions: Observable<any>;

  expandedElement = new Achat();

  stats = [
    { value: 0, display: 'tout' },
    { value: 1, display: 'Supérieur au seuil' },
    { value: -1, display: 'Inférieur au seuil' }
  ];

  constructor(private uow: UowService, public dialog: MatDialog, private mydialog: DeleteService, ) { }

  ngOnInit() {
    this.getPage(new Params());
    // this.getCreditByFournisseur(0);
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
            id: this.idFilter.value !== 0 ? this.idFilter.value : 0,
            // id: 0,
            idCategorie: this.idCategorie.value !== 0 ? this.idCategorie.value : 0,
          }
        );
      }
    );
    //
    // this.autoComplete();
    //
    // this.idFilter.valueChanges.subscribe(() => {
    //   this.getCreditByFournisseur(this.idFilter.value !== 0 ? this.idFilter.value : 0);
    // });
  }

  setColor(a: Article) {
    const color = a.qteStock > a.stockMin ? 'primary' : 'warn';
    return color;
  }

  getPage(data: Params) {
    this.uow.articles.getState(data).subscribe(
      (r: any) => {
        console.log(r.list);
        this.dataSource = r.list;
        this.resultsLength = r.count;
        this.isLoadingResults = false;
      }
    );
  }

  openDialog() {
    const dialogRef = this.dialog.open(PreviewComponent, {
      width: '750px',
      disableClose: true,
      data: { model: null, title: 'Preview' }
    });

    return dialogRef.afterClosed();
  }

  async expandeRow(row: Achat) {
    return;

    if (this.expandedElement.id !== row.id) {
      row = await this.uow.achats.getInfoAchat(row.id).toPromise() as Achat;
      console.log(row)
    }

    return this.expandedElement = this.expandedElement.id === row.id ? new Achat() : row;
  }

  autoComplete() {
    this.filteredOptions = this.myAuto.valueChanges.pipe(
      // startWith(''),
      switchMap((value: string) => value.length > 1 ? this.uow.fournisseurs.autocomplete('nom', value) : []),
      // map(r => r)
    );
  }

  selectChange() {
    this.update.next(true);
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    const o = event.option.value as Client;
    console.log(o);
    this.idFilter.setValue(o.id);
    this.myAuto.setValue(o.nom);
    this.update.next(true);
    // this.idOrganismeEmetteur.setValue(o.id);
  }

  getCreditByFournisseur(id) {
    this.uow.achats.getCreditByFournisseur(id).subscribe((r: any) => {
      console.log(r);
      this.credit = r;
    }
    );
  }

}

export class Params {
  startIndex = 0;
  pageSize = 25;
  sortBy = 'id';
  sortDir = 'desc';
  id = 0;
  idCategorie = 0;
}




