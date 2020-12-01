import { UowService } from 'src/app/services/uow.service';
import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { SousCategorie, Fourniture, Achat } from 'src/app/myModels/models';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  @ViewChild('avance', { static: true }) avance: ElementRef;
  @ViewChild('credit', { static: true }) credit: ElementRef;
  @ViewChild('montant', { static: true }) montant: ElementRef;
  myForm: FormGroup;
  myFormAchat: FormGroup;
  o: Fourniture;
  achat: Achat;
  title = '';
  idFournisseur = new FormControl(0);
  fournisseurs = this.uow.fournisseurs.get();
  constructor(public dialogRef: MatDialogRef<any>, @Inject(MAT_DIALOG_DATA) public data: any
    , private fb: FormBuilder, public uow: UowService) { }

  ngOnInit() {
    this.o = this.data.model;
    this.achat = this.o.achat;
    console.log(this.achat)

    this.montant.nativeElement.value = this.achat.montant;
    this.avance.nativeElement.value = this.achat.avance;
    this.credit.nativeElement.value = this.achat.credit;


    this.title = this.data.title;
    this.idFournisseur.setValue(this.achat.idFournisseur);
    this.createForm();
    this.createFormAchat();

    if (this.o.id === 0) {
      this.myFormAchat.get('modePayement').setValue('éspece');

      // this.myForm.get('titreFr').valueChanges.subscribe(v => {
      //   this.myForm.get('titreAr').setValue(v);
      // })
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  get disabled() {
    const b = this.myForm.get('qte').value === 0 || this.myForm.get('prixAchat').value === 0;

    const b2 = this.myForm.invalid || this.myFormAchat.invalid || this.idFournisseur.value === 0;
    return b || b2;
  }



  createForm() {
    this.myForm = this.fb.group({
      id: this.o.id,
      idArticle: [this.o.idArticle],
      idFournisseur: [this.o.idFournisseur],
      qte: [this.o.qte, Validators.required],
      prixAchat: [this.o.prixAchat, Validators.required],
      dateAchat: [this.o.dateAchat],
    });
  }

  createFormAchat() {
    this.myFormAchat = this.fb.group({
      id: this.achat.id,
      idFournisseur: [this.achat.idFournisseur],
      montant: [this.achat.montant],
      modePayement: [this.achat.modePayement, Validators.required],
      numCheque: [this.achat.numCheque],
      credit: [this.achat.credit],
      avance: [this.achat.avance],
      date: [this.achat.date],
    });
  }

  async onOkClick(newFourniture: Fourniture, newAchat: Achat) {
    // console.log(this.montant.nativeElement.value)
    // return
    newFourniture.idFournisseur = this.idFournisseur.value;
    newAchat.idFournisseur = this.idFournisseur.value;
    // newAchat.montant = +this.myForm.get('qte').value * +this.myForm.get('prixAchat').value;
    newAchat.montant = +this.montant.nativeElement.value;
    newAchat.avance = +this.avance.nativeElement.value;
    newAchat.credit = +this.credit.nativeElement.value;
    console.log(newAchat.credit);
    console.log(newAchat);
    // return;
    if (this.o.id === 0) {
      newFourniture.achat = newAchat;
      await this.uow.fournitures.post(newFourniture).toPromise();
      await this.uow.articles.updateQte(newFourniture.idArticle, +newFourniture.qte).toPromise();
      await this.uow.articles.updateDateLastBuy(newFourniture.idArticle).toPromise();
      this.dialogRef.close(true);

    } else {
      newFourniture.achat = null;
      newFourniture.idAchat = newAchat.id;
      await this.uow.fournitures.put(newFourniture.id, newFourniture).toPromise();
      await this.uow.achats.put(newAchat.id, newAchat).toPromise();
      // console.log(+newFourniture.qte - this.o.qte)
      await this.uow.articles.updateQte(newFourniture.idArticle, +newFourniture.qte - this.o.qte).toPromise();
      await this.uow.articles.updateDateLastBuy(newFourniture.idArticle).toPromise();
      this.dialogRef.close(true);
    }
  }

  resetForm() {
    this.o = new Fourniture();
    this.createForm();
  }

}
