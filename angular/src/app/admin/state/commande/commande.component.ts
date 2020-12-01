import { Article } from './../../../myModels/models';
import { SessionService } from './../../../shared/session.service';


import { Component, OnInit, ViewChild, EventEmitter, Output, Input } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatAutocompleteSelectedEvent } from '@angular/material';
import { merge, Observable } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { SelectionModel, SelectionChange } from '@angular/cdk/collections';
import { FormControl } from '@angular/forms';
import { switchMap, startWith } from 'rxjs/operators';
import { Fournisseur } from 'src/app/myModels/models';
import { PdfCommandeService } from './pdf.commande.service';

@Component({
  selector: 'app-commande',
  templateUrl: './commande.component.html',
  styleUrls: ['./commande.component.scss']
})
export class CommandeComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @Output() eventToParent = new EventEmitter<any[]>();

  @Input() selectedList: ModelList[] = [];
  @Input() private listFromParent = new EventEmitter();
  update = new EventEmitter();
  isLoadingResults = true;
  resultsLength = 0;
  isRateLimitReached = false;

  panelOpenState = false;

  dataSource = [];
  columnDefs = [
    { columnDef: 'select', headName: '' },
    { columnDef: 'article', headName: '' },
    { columnDef: 'prixMin', headName: 'prix min' },
    { columnDef: 'qte', headName: '' }, // refresh
    { columnDef: 'qteStock', headName: 'qte Stock' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName.toUpperCase();
    return e;
  });

  displayedColumns = this.columnDefs.map(e => e.columnDef);

  selection = new SelectionModel<ModelList>(true, []);

  idModelList = 0;

  idCategorie = new FormControl(0);
  // article = new FormControl('');

  categories = this.uow.categories.get();
  // filter
  myAuto = new FormControl('');
  filteredOptions: Observable<any>;
  idFournisseur = new FormControl(0);

  myArticle = new FormControl('');
  articleFilteredOptions: Observable<any>;
  idArticle = new FormControl(0);

  constructor(private uow: UowService, private pdf: PdfCommandeService, private session: SessionService) { }

  ngOnInit() {
    // console.log(this.selection.hasValue());
    // console.log(this.isAllSelected());
    this.listFromParent.subscribe(r => {
      this.selectedList = r;
      this.selectedList.forEach(row => {
        this.selection.select(row);
      });
    });

    merge(...[this.sort.sortChange, this.paginator.page, this.update]).pipe(startWith(null as any)).subscribe(
      r => {
        r === true ? this.paginator.pageIndex = 0 : r = r;
        !this.paginator.pageSize ? this.paginator.pageSize = 25 : r = r;
        const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.isLoadingResults = true;
        this.getPage(
          startIndex,
          this.paginator.pageSize,
          this.sort.active ? this.sort.active : 'id',
          this.sort.direction ? this.sort.direction : 'desc',
          this.idFournisseur.value,
          this.idCategorie.value,
          this.idArticle.value,
        );
      }
    );

    this.autoComplete();
    this.autoCompleteArticle();
  }


  getPage(startIndex, pageSize, sortBy, sortDir, idFournisseur, idCategorie, article) {
    this.uow.articles.getForCommande(startIndex, pageSize, sortBy, sortDir, idFournisseur, idCategorie, article).subscribe(
      (r: any) => {
        console.log(r.list);
        this.dataSource = r.list;
        this.resultsLength = r.count;
        this.isLoadingResults = false;
        //
        // this.selection.clear();
        // this.selectedList.forEach(row => {
        //   this.selection.select(row);
        // } );
      }
    );
  }

  get disable() {
    return this.selectedList.length === 0;
  }

  refresh() {
    this.selectedList = [];
  }

  imprimer(b) {
    this.pdf.generatePdf(this.selectedList, 'A5', this.session.user.nomComplete, this.myAuto.value, b)
  }

  autoComplete() {
    this.filteredOptions = this.myAuto.valueChanges.pipe(
      // startWith(''),
      switchMap((value: string) => value.length > 1 ? this.uow.fournisseurs.autocomplete('societe', value) : []),
      // map(r => r)
    );
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    const o = event.option.value as Fournisseur;
    console.log(o);
    this.idFournisseur.setValue(o.id);
    this.myAuto.setValue(o.societe);
    this.update.next(true);
    // this.idOrganismeEmetteur.setValue(o.id);
  }

  autoCompleteArticle() {
    this.articleFilteredOptions = this.myArticle.valueChanges.pipe(
      // startWith(''),
      switchMap((value: string) => value.length > 1 ? this.uow.articles.autocomplete('titreFr', value) : []),
      // map(r => r)
    );
  }

  selectedArticle(event: MatAutocompleteSelectedEvent): void {
    const o = event.option.value as Article;
    console.log(o);
    this.idArticle.setValue(o.id);
    this.myArticle.setValue(o.titreFr);
    this.update.next(true);
    // this.idOrganismeEmetteur.setValue(o.id);
  }

  selectChange() {
    // console.lo
    this.update.next(true);
  }

  change(row: ModelList, q: number) {
    console.log(q)
    this.selectedList.forEach(e => {
      if (e.id === row.id) {
        e.qte = +q;
      }
    });

    console.log(this.selectedList)
  }

  getValue(id) {
    const i = this.selectedList.findIndex(o => id === o.id);
    const existe: boolean = i !== -1;
    if (existe) {
      return this.selectedList[i].qte;
    }

    return 0;
  }

  clearFr() {
    this.idFournisseur.setValue(0);
    this.update.next(true);
  }

  clearCat() {
    this.idCategorie.setValue(0);
    this.update.next(true);
  }


  isSelected(row): boolean {
    return this.selectedList.find(e => e.id === row.id) ? true : false;
  }

  check(r) {
    const i = this.selectedList.findIndex(o => r.id === o.id);
    const existe: boolean = i !== -1;
    if (!existe) {
      this.selectedList.push(r);
    } else {
      this.selectedList.splice(i, 1);
    }
    // this.eventToParent.next(this.selectedList);
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.paginator.pageSize;
    // console.log(numSelected, numRows)
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: ModelList): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    // return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id}`;
    return `${this.selectedList.find(e => e.id === row.id) ? 'deselect' : 'select'} row ${row.id}`;
  }

  reset() {
    this.idFournisseur.setValue(0);
    this.update.next(true);
    this.myAuto.setValue('');
  }

}

export interface ModelList {
  id: number;
  article: string;
  prixMin: number;
  qte: number;
}
