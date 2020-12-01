
  import { UowService } from 'src/app/services/uow.service';
  import { Component, OnInit, Inject } from '@angular/core';
  import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
  import { FormGroup, FormBuilder, Validators } from '@angular/forms';
  import { SousCategorie, Fourniture } from 'src/app/myModels/models';

  @Component({
    selector: 'app-edit',
    templateUrl: './edit.component.html',
    styleUrls: ['./edit.component.scss']
  })
  export class EditComponent implements OnInit {
    myForm: FormGroup;
    o: Fourniture;
    title = '';
    fournisseurs = this.uow.fournisseurs.get();
    constructor(public dialogRef: MatDialogRef<any>, @Inject(MAT_DIALOG_DATA) public data: any
      , private fb: FormBuilder, private uow: UowService) { }

    ngOnInit() {
      this.o = this.data.model;
      this.title = this.data.title;
      console.log(this.o)
      this.createForm();
    }

    onNoClick(): void {
      this.dialogRef.close();
    }

    onOkClick(o: any): void {
      this.dialogRef.close(o);
    }

    createForm() {
      this.myForm = this.fb.group({
        id: this.o.id,
        idArticle: [this.o.idArticle],
        idFournisseur: [this.o.idFournisseur, Validators.required],
        qte: [this.o.qte, Validators.required],
        prixAchat: [this.o.prixAchat, Validators.required],
        dateAchat: [this.o.dateAchat],
        article: [this.o.article],
        fournisseur: [this.o.fournisseur],
      });
    }

    resetForm() {
      this.o = new Fourniture();
      this.createForm();
    }

  }

