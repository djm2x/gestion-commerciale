import { FormControl } from '@angular/forms';
import { Component, OnInit, ViewChild, EventEmitter, Input } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatAutocompleteSelectedEvent, MatTableDataSource } from '@angular/material';
import { merge, Observable } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { DeleteService } from 'src/app/layouts/delete/delete.service';
import { Fourniture, Article, Client } from 'src/app/myModels/models';
import { switchMap, filter, startWith } from 'rxjs/operators';
import { FilterService } from '../filter.service';

@Component({
  selector: 'app-three',
  templateUrl: './three.component.html',
  styleUrls: ['./three.component.scss'],
})
export class ThreeComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  // @Input() o: Article;
  dataSource = new MatTableDataSource([]);
  update = new EventEmitter();

  isLoadingResults = true;
  resultsLength = 0;

  isRateLimitReached = false;
  panelOpenState = false;

  columnDefs = [
    { columnDef: 'article', headName: '' },
    { columnDef: 'count', headName: 'Nbr de vente' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName;
    return e;
  });

  displayedColumns = this.columnDefs.map(e => e.columnDef);

  idCategorie = new FormControl(0);
  year = new FormControl(2020);
  categories = this.uow.categories.get();
  constructor(public uow: UowService, public dialog: MatDialog
    , private mydialog: DeleteService, private filter: FilterService ) { }

  ngOnInit() {
    // this.getPage(this.year.value, this.idCategorie.value);
    this.filter.year.subscribe(r => {
      this.year.setValue(r);
      console.log('subjet year')
      this.update.next(true);
    });

    this.filter.idCategorie.subscribe(r => {
      this.idCategorie.setValue(r);
      console.log('subjet idcategorie')
      this.update.next(true);
    });

    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;

    merge(...[this.update]).pipe(startWith(null as any)).subscribe(
      r => {
        this.isLoadingResults = true;
        this.getPage(
            this.year.value !== 0 ? this.year.value : 0,
            this.idCategorie.value !== 0 ? this.idCategorie.value : 0
        );
      }
    );

  }

  // setColor(a: Article) {
  //   const color = a.qteStock > a.stockMin ? 'primary' : 'warn';
  //   return color;
  // }

  getPage(year, idCategorie) {
    this.uow.articles.getTop50Sell(year, idCategorie).subscribe((r: any) => {
      console.log(r);
      // const months = this.uow.monthsAlphaMin;
      this.dataSource.data = r.list;

      this.resultsLength = r.count;
      this.isLoadingResults = false;
    }
    );
  }



  selectChange() {
    this.update.next(true);
  }



}





