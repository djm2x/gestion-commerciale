import { Component, OnInit, ViewChild, EventEmitter } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { merge, Observable, pipe } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { SnackbarService } from 'src/app/shared/snakebar.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SessionService } from 'src/app/shared';
import { Article, Fourniture } from 'src/app/myModels/models';
import { DeleteService } from 'src/app/layouts/delete/delete.service';
import { startWith } from 'rxjs/operators';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  // @ViewChild('myAutocomplete', { static: true }) myAutocomplete: MatInput;
  update = new EventEmitter();
  panelOpenState = true;
  isLoadingResults = true;
  resultsLength = 0;
  isRateLimitReached = false;
  dataSource = [];
  columnDefs = [
    { columnDef: 'code', headName: '' },
    { columnDef: 'titreFr', headName: 'FR' },
    { columnDef: 'titreAr', headName: 'AR' },
    { columnDef: 'prixUnitaire', headName: 'PU' },
    { columnDef: 'qteStock', headName: 'Q' },
    { columnDef: 'unite', headName: 'UNITE' },
    { columnDef: 'option', headName: '' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName;
    return e;
  });
  //
  displayedColumns = this.columnDefs.map(e => e.columnDef);
  categories = this.uow.categories.get();

  critere = new FormControl('');
  categorie = new FormControl(0);
  fournisseur = new FormControl('');

  constructor(private uow: UowService, private snack: SnackbarService, private fb: FormBuilder
    , private route: ActivatedRoute, private mydialog: DeleteService) { }

  ngOnInit() {
    let o2 = this.loadSearchFromLocalstorage();
    if (o2 === null) {
      o2 = {
        startIndex: 0,
        pageSize: 20,
        sortBy: 'id',
        sortDir: 'desc',
        critere: '',
        fournisseur: '',
        categorie: 0,
      };
    } else {
      this.paginator.pageSize = o2.pageSize;
      this.paginator.pageIndex = o2.startIndex / this.paginator.pageSize;
      this.sort.active = o2.sortBy;
      this.sort.direction = o2.sortDir;
      this.critere.setValue(o2.critere);
      this.fournisseur.setValue(o2.fournisseur);
      this.categorie.setValue(o2.categorie);
    }
    this.getPage(o2);
    merge(...[this.sort.sortChange, this.paginator.page, this.update]).pipe(startWith(null)).subscribe(
      r => {
        r === true ? this.paginator.pageIndex = 0 : r = r;
        !this.paginator.pageSize ? this.paginator.pageSize = 20 : r = r;
        const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.isLoadingResults = true;
        // console.log('ttttttttttttttttttttt')
        const o = {
          startIndex,
          pageSize: this.paginator.pageSize,
          sortBy: this.sort.active ? this.sort.active : 'id',
          sortDir: this.sort.direction ? this.sort.direction : 'desc',
          critere: this.critere.value,
          fournisseur: this.fournisseur.value,
          categorie: this.categorie.value
        };

        this.saveSearchInLocalstorage(o);

        this.getPage(o);
      }
    );
  }

  saveSearchInLocalstorage(o) {
    localStorage.setItem('DATA_SEARCH', (JSON.stringify(o)));
  }

  loadSearchFromLocalstorage() {
    return JSON.parse(localStorage.getItem('DATA_SEARCH'));
  }

  search() {
    this.update.next(true);
  }

  reset() {
    this.critere.setValue('');
    this.fournisseur.setValue('');
    this.categorie.setValue(0);
    localStorage.removeItem('DATA_SEARCH');
    this.update.next(true);
  }


  getPage(o) {
    console.log(o)
    this.uow.articles.getAndSearch(o).subscribe(
      (r: any) => {
        console.log(r.list);
        this.dataSource = r.list;
        this.resultsLength = r.count;
        this.isLoadingResults = false;
      }
    );
  }

  async delete$(o: Article) {
    const r = await this.mydialog.openDialog('Article').toPromise();
    if (r === 'ok') {
      this.uow.articles.delete(o.id).subscribe(() => {
        this.uow.files.deleteFiles([o.image], 'articles').subscribe(() => {
          this.update.next(true);
        });
      }, e => console.warn(e));
    }
  }

  // axeChange(idAxe: number) {
  //   this.uow.sousAxes.getByIdAxe(idAxe).subscribe(r => {
  //     this.sousAxes = r as any[];
  //   });
  // }

  


}

class Search {
  startIndex = 0;
  pageSize = 0;
  sortBy = '';
  sortDir = '';
  critere = '';
  fournisseur = '';
  categorie = 0;
}


