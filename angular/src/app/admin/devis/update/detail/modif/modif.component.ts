import { UowService } from 'src/app/services/uow.service';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DevisArticle } from 'src/app/myModels/models';

@Component({
  selector: 'app-modif',
  templateUrl: './modif.component.html',
  styleUrls: ['./modif.component.scss']
})
export class ModifComponent implements OnInit {
  myForm: FormGroup;
  o: DevisArticle;
  title = '';
  remises = [...Array(5).keys()].map(e => {
    return {value: e * 5, display: `${e * 5} %`};
  });
  fournisseurs = this.uow.fournisseurs.get();
  constructor(public dialogRef: MatDialogRef<any>, @Inject(MAT_DIALOG_DATA) public data: any
    , private fb: FormBuilder, private uow: UowService) { }

  ngOnInit() {
    this.o = this.data.model;
    this.title = this.data.title;
    this.createForm();
    console.log(this.o)
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onOkClick(o: DevisArticle): void {
    o.total = (+o.qte * +o.pu) * (1 - o.remise / 100);
    this.dialogRef.close(o);
  }

  createForm() {
    this.myForm = this.fb.group({
      id: [this.o.id],
      idArticle: [this.o.idArticle],
      idDevis: [this.o.idDevis],
      pu: [this.o.pu, Validators.required],
      qte: [this.o.qte, Validators.required],
      remise: [this.o.remise, Validators.required],
      total: [this.o.total, Validators.required],
    });
  }

  resetForm() {
    this.o = new DevisArticle();
    this.createForm();
  }

}

