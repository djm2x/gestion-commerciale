import { Article, Fourniture } from './../../../../../myModels/models';
import { UowService } from 'src/app/services/uow.service';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatAutocompleteSelectedEvent } from '@angular/material';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-modif',
  templateUrl: './modif.component.html',
  styleUrls: ['./modif.component.scss']
})
export class ModifComponent implements OnInit {
  myForm: FormGroup;
  o: Fourniture;
  title = '';
  // remises = [...Array(5).keys()].map(e => {
  //   return {value: e * 5, display: `${e * 5} %`};
  // });
  fournisseurs = this.uow.fournisseurs.get();
  myAuto = new FormControl('');
  filteredOptions: Observable<any>;
  
  constructor(public dialogRef: MatDialogRef<any>, @Inject(MAT_DIALOG_DATA) public data: any
    , private fb: FormBuilder, private uow: UowService) { }

  ngOnInit() {
    this.o = this.data.model;
    this.title = this.data.title;
    this.createForm();
    console.log(this.o)

    this.myAuto.setValue((this.o as any).titreFr ? (this.o as any).titreFr : '');

    this.autoComplete();
  }

  autoComplete() {
    this.filteredOptions = this.myAuto.valueChanges.pipe(
      // startWith(''),
      switchMap((value: string) => value.length > 1 ? this.uow.articles.autocomplete('titreFr', value) : []),
      // map(r => r)
    );
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    const o = event.option.value as Article;
    console.log(o);
    this.myForm.get('idArticle').setValue(o.id);
    this.myAuto.setValue(o.titreFr);
    // this.update.next(true);
    // this.idOrganismeEmetteur.setValue(o.id);
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onOkClick(o: Fourniture): void {
    // o.total = (+o.qte * +o.prixAchat)/* * (1 - o.remise / 100)*/;
    this.dialogRef.close(o);
  }

  get disable() {
    // console.log(this.myAuto.value)
    return this.myForm.get('prixAchat').value === 0 || this.myForm.get('qte').value === 0 
    || this.myForm.invalid || this.myAuto.value === '';
  }

  createForm() {
    this.myForm = this.fb.group({
      id: [this.o.id, Validators.required],
      idArticle: [this.o.idArticle],
      idAchat: [this.o.idAchat],
      prixAchat: [this.o.prixAchat, Validators.required],
      qte: [this.o.qte, Validators.required],
      dateAchat: [this.o.dateAchat, Validators.required],
      idFournisseur: [this.o.idFournisseur, Validators.required],
    });
  }

  resetForm() {
    this.o = new Fourniture();
    this.createForm();
  }

  

}

