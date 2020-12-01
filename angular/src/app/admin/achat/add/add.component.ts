import { Fournisseur, Article } from './../../../myModels/models';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit, Inject } from '@angular/core';
import { UowService } from 'src/app/services/uow.service';
import { Fourniture } from 'src/app/myModels/models';
import { MatDialogRef, MAT_DIALOG_DATA, MatAutocompleteSelectedEvent } from '@angular/material';
import { DeleteService } from 'src/app/layouts/delete/delete.service';
import { EventService } from '../event.service';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  categories = this.uow.categories.get();
  fourns = this.uow.fournisseurs.get();
  articles = [];
  // fournisseur = new FormControl(0);
  idCategorie = new FormControl(0);
  //
  myAuto = new FormControl('');
  filteredOptions: Observable<any>;
  //
  myForm: FormGroup;
  o = new Fourniture();
  constructor(private uow: UowService, private fb: FormBuilder
    , public dialogRef: MatDialogRef<any>, @Inject(MAT_DIALOG_DATA) public data: any
    , private mydialog: DeleteService, private event: EventService) { }

  ngOnInit() {
    this.createForm();

    this.autoComplete();

    this.event.resetForm.subscribe(r => {
      this.o = new Fourniture();
      this.createForm();
      this.myAuto.setValue('');
    })
  }

  createForm() {
    this.myForm = this.fb.group({
      id: this.o.id,
      article: [this.o.article, Validators.required],
      fournisseur: [this.o.fournisseur, Validators.required],
      qte: [this.o.qte, Validators.required],
      prixAchat: [0 /*this.o.prixAchat*/, Validators.required],
      prixUnitaire: [this.o.article.prixUnitaire, Validators.required],
      dateAchat: [this.o.dateAchat],
    });
  }


  get isEmpty() {
    return this.myForm.get('article').value.id === 0 || this.myForm.invalid;
  }

  filter() {
    const fns = this.myForm.get('fournisseur').value as Fournisseur;
    this.uow.articles.filterBy(fns.id ? fns.id : 0, this.idCategorie.value).subscribe(r => {
      console.log(r)
      this.articles = r as any[];
    });
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
    this.myForm.get('fournisseur').setValue(o);
    
    this.myAuto.setValue(o.societe);
    this.filter();
    // this.update.next(true);
    // this.idOrganismeEmetteur.setValue(o.id);
  }

  setArticle(e: Article) {
    console.log(e);
    this.myForm.get('prixUnitaire').setValue(e.prixUnitaire);
    this.myForm.get('qte').setValue(1);
  }

  submit(f: Fourniture) {
    f.idFournisseur = f.fournisseur.id;
    f.idArticle = f.article.id;
    console.log(f);
    this.event.add.next(f);
    // this.uow.fournitures.post(f).subscribe(r => {
    //   console.log(r);
    //   this.uow.articles.updateQte(f.idArticle, +f.qte).subscribe(e => {
    //     this.event.add.next(false);
    //   });
    // });
  }

}
