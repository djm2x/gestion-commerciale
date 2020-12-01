import { Component, OnInit, Inject, ViewChild, Input, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatPaginator, MatSort, MatAutocompleteSelectedEvent } from '@angular/material';
import { FormBuilder, FormControl } from '@angular/forms';
import { UowService } from 'src/app/services/uow.service';
import { Article, Achat, Fournisseur } from 'src/app/myModels/models';
import { Observable, merge } from 'rxjs';
import { switchMap, startWith, delay } from 'rxjs/operators';

@Component({
  selector: 'app-add-avance',
  templateUrl: './add-avance.component.html',
  styleUrls: ['./add-avance.component.scss']
})
export class AddAvanceComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  o: any;
  title = '';

  update = new EventEmitter();
  isLoadingResults = false;
  resultsLength = 0;
  isRateLimitReached = false;
  panelOpenState = false;
  dataSource: Achat[] = [];
  columnDefs = [
    { columnDef: 'date', headName: '' },
    { columnDef: 'montant', headName: '' },
    { columnDef: 'credit', headName: '' },
    { columnDef: 'avance', headName: '' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName;
    return e;
  });

  idFournisseur = new FormControl(0);

  avance = new FormControl(0);

  displayedColumns = this.columnDefs.map(e => e.columnDef);

  // credit = 0;
  // avance = 0;

  myAuto = new FormControl('');
  filteredOptions: Observable<any>;

  constructor(public dialogRef: MatDialogRef<any>, @Inject(MAT_DIALOG_DATA) public data: any
    , private fb: FormBuilder, private uow: UowService) { }

  ngOnInit() {
    // this.o = this.data.model;
    this.title = this.data.title;
    //
    merge(...[this.sort.sortChange, this.paginator.page, this.update]).pipe(delay(500)).subscribe(
      r => {
        r === true ? this.paginator.pageIndex = 0 : r = r;
        !this.paginator.pageSize ? this.paginator.pageSize = 25 : r = r;
        const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.isLoadingResults = true;
        this.get(
          startIndex,
          this.paginator.pageSize,
          this.sort.active ? this.sort.active : 'id',
          this.sort.direction ? this.sort.direction : 'desc',
          this.idFournisseur.value !== 0 ? this.idFournisseur.value : 0);
      }
    );
    //
    this.autoComplete();
  }

  get(startIndex, pageSize, sortBy, sortDir, idFournisseur) {
    this.isLoadingResults = true;
    this.uow.achats.getAchatsWithCredit(startIndex, pageSize, sortBy, sortDir, idFournisseur).subscribe(
      (r: any) => {
        // console.log(r.list);
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

  reset() {
    this.idFournisseur.setValue(0);
    this.update.next(true);
    this.myAuto.setValue('');
  }

  async submit() {
    let paye = +this.avance.value;
    // console.log(this.dataSource);

    this.dataSource.forEach(e => {

      console.log(paye)
      if (paye >= e.credit && e.credit !== 0) {
        paye -= e.credit - e.avance;
        e.avance = e.credit;
        e.credit = 0;
      } else if (paye < e.credit && paye !== 0  && e.credit !== 0) {
        e.credit -= paye;
        e.avance += paye;
        paye = 0;
      }
    });

    await this.uow.achats.updateRange(this.dataSource).toPromise();

    // console.log(this.dataSource)
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  get disable() {
    // console.log(this.myAuto.value)
    return this.idFournisseur.value === 0 || this.avance.value === 0;
  }


}
