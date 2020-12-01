import { Client } from './../../../myModels/models';
import { PdfService } from './../../pdf.service';
import { Commande } from '../../../myModels/models';
import { ActivatedRoute } from '@angular/router';

import { Component, OnInit, ViewChild, EventEmitter } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatAutocompleteSelectedEvent } from '@angular/material';
import { merge, Observable } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { SnackbarService } from 'src/app/shared/snakebar.service';
import { DetailsComponent } from '../details/details.component';
import { FormControl } from '@angular/forms';
import { SessionService } from 'src/app/shared';
import { DeleteService } from 'src/app/layouts/delete/delete.service';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  update = new EventEmitter();
  isLoadingResults = true;
  resultsLength = 0;
  isRateLimitReached = false;
  dataSource = [];
  columnDefs = [
    // { columnDef: 'id', headName: 'id' },
    { columnDef: 'date', headName: '' },
    { columnDef: 'client', headName: '' },
    { columnDef: 'total', headName: '' },
    { columnDef: 'modePayement', headName: 'MODE PAYEMENT' },
    { columnDef: 'avance', headName: '' },
    { columnDef: 'credit', headName: '' },
    { columnDef: 'option', headName: 'OPTION' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName;
    return e;
  });


  panelOpenState = false;

  clients = this.uow.clients.get();
  idClient = new FormControl(0);
  displayedColumns = this.columnDefs.map(e => e.columnDef);

  credit = 0;
  avance = 0;

  d1 = new FormControl(this.getDaysAgo(30));
  d2 = new FormControl(new Date());

  myAuto = new FormControl('');
  filteredOptions: Observable<any>;


  constructor(private uow: UowService, public dialog: MatDialog, private mydialog: DeleteService
    , private snack: SnackbarService, private route: ActivatedRoute, public session: SessionService,
    public pdf: PdfService) { }

  ngOnInit() {
    this.getPage({
      startIndex: 0,
      pageSize: 10,
      sortBy: 'id',
      sortDir: 'desc',
      d1: this.d1.value,
      d2: this.d2.value,
      idClient: 0,
    });

    merge(...[this.sort.sortChange, this.paginator.page, this.update]).subscribe(
      r => {
        r === true ? this.paginator.pageIndex = 0 : r = r;
        !this.paginator.pageSize ? this.paginator.pageSize = 10 : r = r;
        const startIndex = this.paginator.pageIndex * this.paginator.pageSize;

        // console.log(this.d1.value); 
        // return;
        this.getPage(
          {
            startIndex,
            pageSize: this.paginator.pageSize,
            sortBy: this.sort.active ? this.sort.active : 'id',
            sortDir: this.sort.direction ? this.sort.direction : 'desc',
            d1: this.valideDate(this.d1.value),
            d2: this.valideDate(this.d2.value),
            idClient: this.idClient.value !== 0 ? this.idClient.value : 0
          }
        );
      }
    );




    // this.route.queryParams.subscribe(params => {
    //   const id = params['data'];
    //   if (id) {
    //     this.uow.commandes.getDetail(id).subscribe(r => {
    //       this.openDialog(r);
    //     });
    //   }
    // });

    this.autoComplete();
  }

  getDaysAgo(days: number): Date {
    const toDay = new Date();

    const t: number = toDay.setDate(toDay.getDate() - days);

    return new Date(t);
  }

  getPage({ startIndex, pageSize, sortBy, sortDir, d1, d2, idClient }) {
    this.isLoadingResults = true;
    console.log({ startIndex, pageSize, sortBy, sortDir, d1, d2, idClient }.d1)
    this.uow.commandes.getAllByDate({ startIndex, pageSize, sortBy, sortDir, d1, d2, idClient }).subscribe(
      (r: any) => {
        console.log(r.list);
        this.dataSource = r.list;
        this.resultsLength = r.count;
        this.credit = r.credit;
        this.avance = r.avance;
        this.isLoadingResults = false;
      }
    );
  }

  dateChange(d) {
    console.log(d)
  }

  valideDate(date: Date): Date {
    date = new Date(date);

    const hoursDiff = date.getHours() - date.getTimezoneOffset() / 60;
    const minutesDiff = (date.getHours() - date.getTimezoneOffset()) % 60;
    date.setHours(hoursDiff);
    date.setMinutes(minutesDiff);

    return date;
  }

  autoComplete() {
    this.filteredOptions = this.myAuto.valueChanges.pipe(
      // startWith(''),
      switchMap((value: string) => value.length > 1 ? this.uow.clients.autocomplete('nom', value) : []),
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
    this.idClient.setValue(o.id);
    this.myAuto.setValue(o.nom);
    // this.update.next(true);
    // this.idOrganismeEmetteur.setValue(o.id);
  }


  async delete(o: Commande) {
    const r = await this.mydialog.openDialog('Detail commande').toPromise();
    if (r === 'ok') {
      console.log(o)
      o.detailCmds.forEach(async e => {
        await this.uow.articles.updateQte(e.idArticle, +e.qtePrise).toPromise();
      });
      this.uow.commandes.delete(o.id).subscribe(() => {
        this.update.next(true);
      });
    }
  }

  detail(o) {
    this.uow.commandes.getOne(o.id).subscribe(r => {
      this.openDialog(r);
      // console.log(r);
    });
  }

  getPdf(o, format) {
    this.uow.commandes.getOne(o.id).subscribe(r => {
      console.log(o)
      this.pdf.generatePdf(r, format, this.session.user.nomComplete);
    });
  }

  search() {
    // if (Number(this.idTraite.value) === 0 && Number(this.idRapport.value) === 0 && Number(this.idOrganismeEmetteur.value) === 0) {
    //   return;
    // }
    this.update.next(true);
  }

  openDialog(o: any) {
    const dialogRef = this.dialog.open(DetailsComponent, {
      width: '7100px',
      disableClose: true,
      data: { model: o, title: 'Detail commande' }
    });

    return dialogRef.afterClosed();
  }

  // selectChange(idTraite: number) {
  //   this.uow.rapports.getByIdTraite(idTraite).subscribe(r => {
  //     this.rapports = r as any[];
  //     console.log(r);
  //   });
  // }



  // get isAllEmpty(): boolean {
  //   if (this.idTraite.value === 0 && this.idRapport.value === 0 && this.idOrganismeEmetteur.value === 0) {
  //     return true;
  //   }
  //   return false;
  // }

  reset() {
    this.d1.setValue(new Date());
    this.d2.setValue(new Date());
    this.idClient.setValue(0);
    this.update.next(true);
  }

}


