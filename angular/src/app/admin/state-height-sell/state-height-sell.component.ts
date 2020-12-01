import { PreviewComponent } from './preview/preview.component';
import { Achat } from './../../myModels/models';
import { FormControl } from '@angular/forms';
import { Component, OnInit, ViewChild, EventEmitter, Input } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatAutocompleteSelectedEvent, MatTableDataSource } from '@angular/material';
import { merge, Observable } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { DeleteService } from 'src/app/layouts/delete/delete.service';
import { Fourniture, Article, Client } from 'src/app/myModels/models';
import { switchMap, filter } from 'rxjs/operators';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { FilterService } from './filter.service';

@Component({
  selector: 'app-state-stock',
  templateUrl: './state-height-sell.component.html',
  styleUrls: ['./state-height-sell.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class StateHeightSellComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @Input() o: Article;
  update = new EventEmitter();
  isLoadingResults = true;
  resultsLength = 0;
  isRateLimitReached = false;
  panelOpenState = false;
  dataSource = new MatTableDataSource([]);
  columnDefs = [
    { columnDef: 'titre', headName: '' },
    { columnDef: this.uow.monthsAlphaMin[0].name, headName: '' },
    { columnDef: this.uow.monthsAlphaMin[1].name, headName: '' },
    { columnDef: this.uow.monthsAlphaMin[2].name, headName: '' },
    { columnDef: this.uow.monthsAlphaMin[3].name, headName: '' },
    { columnDef: this.uow.monthsAlphaMin[4].name, headName: '' },
    { columnDef: this.uow.monthsAlphaMin[5].name, headName: '' },
    { columnDef: this.uow.monthsAlphaMin[6].name, headName: '' },
    { columnDef: this.uow.monthsAlphaMin[7].name, headName: '' },
    { columnDef: this.uow.monthsAlphaMin[8].name, headName: '' },
    { columnDef: this.uow.monthsAlphaMin[9].name, headName: '' },
    { columnDef: this.uow.monthsAlphaMin[10].name, headName: '' },
    { columnDef: this.uow.monthsAlphaMin[11].name, headName: '' },

    // { columnDef: 'option'   , headName: '' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName;
    return e;
  });

  // fournisseurs = this.uow.fournisseurs.get();
  // idFilter = new FormControl(0);
  idCategorie = new FormControl(0);
  year = new FormControl(2020);
  // d1 = new FormControl(new Date());
  displayedColumns = this.columnDefs.map(e => e.columnDef);

  credit = 0;
  categories = this.uow.categories.get();
  // myAuto = new FormControl('');
  // filteredOptions: Observable<any>;

  expandedElement = new Achat();

  // stats = [
  //   { value: 0, display: 'tout' },
  //   { value: 1, display: 'Supérieur au seuil' },
  //   { value: -1, display: 'Inférieur au seuil' }
  // ];

  constructor(public uow: UowService, public dialog: MatDialog
    , private mydialog: DeleteService, private filter: FilterService) { }

  ngOnInit() {
    this.getPage(0, 0);
    // this.getCreditByFournisseur(0);
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;

    this.year.valueChanges.subscribe(r => {
      this.filter.year.next(this.year.value);
    });

    this.idCategorie.valueChanges.subscribe(r => {
      this.filter.idCategorie.next(this.idCategorie.value);
    });
    
    merge(...[this.update]).subscribe(
      r => {
        
        this.isLoadingResults = true;
        this.getPage(
            this.year.value !== 0 ? this.year.value : 0,
            this.idCategorie.value !== 0 ? this.idCategorie.value : 0
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

  getPage(year, idCategorie) {
    this.uow.articles.getHeightSell(year, idCategorie).subscribe(
      (r: {list: any[], count: number}) => {
        const months = this.uow.monthsAlphaMin;
        this.dataSource.data = r.list.map(e => {
          return {
            titre: e.titre,
            [months[0].name]: (e.months as number[]).filter(f => f === 1).length,
            [months[1].name]: (e.months as number[]).filter(f => f === 2).length,
            [months[2].name]: (e.months as number[]).filter(f => f === 3).length,
            [months[3].name]: (e.months as number[]).filter(f => f === 4).length,
            [months[4].name]: (e.months as number[]).filter(f => f === 5).length,
            [months[5].name]: (e.months as number[]).filter(f => f === 6).length,
            [months[6].name]: (e.months as number[]).filter(f => f === 7).length,
            [months[7].name]: (e.months as number[]).filter(f => f === 8).length,
            [months[8].name]: (e.months as number[]).filter(f => f === 9).length,
            [months[9].name]: (e.months as number[]).filter(f => f === 10).length,
            [months[10].name]: (e.months as number[]).filter(f => f === 11).length,
            [months[11].name]: (e.months as number[]).filter(f => f === 12).length,
          };
        });

        
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

  // autoComplete() {
  //   this.filteredOptions = this.myAuto.valueChanges.pipe(
  //     // startWith(''),
  //     switchMap((value: string) => value.length > 1 ? this.uow.fournisseurs.autocomplete('nom', value) : []),
  //     // map(r => r)
  //   );
  // }

  selectChange() {
   
    this.update.next(true);
  }

  // selected(event: MatAutocompleteSelectedEvent): void {
  //   const o = event.option.value as Client;
  //   console.log(o);
  //   this.idFilter.setValue(o.id);
  //   this.myAuto.setValue(o.nom);
  //   this.update.next(true);
  //   // this.idOrganismeEmetteur.setValue(o.id);
  // }

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




