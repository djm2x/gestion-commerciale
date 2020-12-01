import { Router } from '@angular/router';
import { Achat } from './../../../myModels/models';
import { EditComponent } from './../edit/edit.component';

import { Component, OnInit, ViewChild, EventEmitter, Input } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatTableDataSource } from '@angular/material';
import { merge } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { DeleteService } from 'src/app/layouts/delete/delete.service';
import { Fourniture, Article } from 'src/app/myModels/models';
import { AddComponent } from '../../article/update/fourniture/add/add.component';
import { EventService } from '../event.service';
import { delay } from 'rxjs/operators';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { SnackBarService } from 'src/app/loader/snack-bar.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  o = new Achat();
  update = new EventEmitter();
  isLoadingResults = false;
  resultsLength = 0;
  isRateLimitReached = false;

  dataSource = new MatTableDataSource<Fourniture>([]);
  // dataSource = [];
  columnDefs = [
    { columnDef: 'fournisseur', headName: '' },
    { columnDef: 'article', headName: '' },
    { columnDef: 'qte', headName: '' },
    { columnDef: 'prixAchat', headName: '' },
    { columnDef: 'dateAchat', headName: '' },
    { columnDef: 'option', headName: 'OPTION' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName;
    return e;
  });

  displayedColumns = this.columnDefs.map(e => e.columnDef);
  total = 0;
  myForm: FormGroup;
  constructor(public uow: UowService, public dialog: MatDialog, private mydialog: DeleteService
    , private event: EventService, private fb: FormBuilder
    , private snack: SnackBarService, private router: Router) { }

  ngOnInit() {
    this.createForm();
    this.event.add.pipe(delay(0)).subscribe((r: Fourniture) => {
      const i = this.dataSource.data.findIndex(e => e.idArticle === r.idArticle && e.idFournisseur === r.idFournisseur);
      if (i === -1) {
        this.dataSource.data.push(r);
        const l = this.dataSource.data;
        this.dataSource.data = [];
        this.dataSource.data = l;
        this.getTotal();
        console.log(l)
      }
    });

    this.myForm.get('avance').valueChanges.subscribe(v => {
      const montant = +this.myForm.get('montant').value;
      const avance = +this.myForm.get('avance').value;

      this.myForm.get('credit').setValue(montant - avance);
    });

    this.myForm.get('modePayement').setValue('éspece');
  }

  createForm() {
    this.myForm = this.fb.group({
      id: this.o.id,
      idFournisseur: [this.o.idFournisseur, Validators.required],
      montant: [this.o.montant, Validators.required],
      modePayement: [this.o.modePayement, Validators.required],
      numCheque: [this.o.numCheque],
      credit: [this.o.credit],
      avance: [this.o.avance],
      date: [this.o.date],
    });
  }

  openDialog(o: Fourniture, text) {
    const dialogRef = this.dialog.open(EditComponent, {
      width: '750px',
      disableClose: true,
      data: { model: o, title: text }
    });

    return dialogRef.afterClosed();
  }



  edit(o: Fourniture) {
    this.openDialog(o, 'Modifier achat').subscribe((frt: Fourniture) => {
      if (frt) {
        console.log(frt);
        const i = this.dataSource.data.findIndex(e => e.idArticle === frt.idArticle && e.idFournisseur === frt.idFournisseur);
        if (i !== -1) {
          const l = this.dataSource.data;
          this.dataSource.data = [];
          l[i] = frt;
          this.dataSource.data = l;
          this.getTotal();
        }
      }
    });
  }

  async delete(index) {
    const r = await this.mydialog.openDialog('sous-axe').toPromise();
    if (r === 'ok') {
      const l = this.dataSource.data;
      this.dataSource.data = [];
      l.splice(index, 1);
      this.dataSource.data = l;
      this.getTotal();
    }
  }

  getTotal() {
    this.total = 0;
    this.dataSource.data.forEach(e => {
      this.total += e.prixAchat * e.qte;
      this.myForm.get('montant').setValue(this.total);
      this.myForm.get('credit').setValue(this.total);
      this.myForm.get('modePayement').setValue('éspece');
    });
  }

  async submit(achat: Achat) {
    const l = this.dataSource.data;
    // this.dataSource.data = l;
    achat.idFournisseur = this.dataSource.data[0].idFournisseur;
    this.dataSource.data = [];
    achat.montant = this.total;
    // this.dataSource.data = l;
    const newAchat = await this.uow.achats.post(achat).toPromise();

    const articlesPriceUEdit = l.map((e: any) => {
      const a = e.article;
      a.prixUnitaire = e.prixUnitaire;
      return a as Article;
    });

    await this.uow.articles.updateRange(articlesPriceUEdit).toPromise();
    // console.log(l)
    // return
    l.forEach(e => {
      e.article = null;
      e.fournisseur = null;
      e.idAchat = newAchat.id;
    });



    this.uow.fournitures.postRange(l).subscribe(r => {
      console.log(r);
      this.o = new Achat();
      this.createForm();
      this.event.resetForm.next(true);
      this.myForm.get('modePayement').setValue('éspece');

      this.snack.notifyOk('Commande est bien enregitrer');
      this.router.navigate(['/admin/state']);
    });

    console.log(l);
  }

}





