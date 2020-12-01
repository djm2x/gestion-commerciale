import { UowService } from 'src/app/services/uow.service';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { User } from 'src/app/myModels/models';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class UpdateComponent implements OnInit {
  myForm: FormGroup;
  o: User;
  title = '';
  roles = this.uow.roles.get();
  hide = true;
  constructor(public dialogRef: MatDialogRef<any>, @Inject(MAT_DIALOG_DATA) public data: any
  , private fb: FormBuilder, private uow: UowService) { }

  ngOnInit() {
    this.o = this.data.model;
    this.title = this.data.title;
    this.createForm();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onOkClick(o: User): void {
    this.dialogRef.close(o);
  }

  createForm() {
    this.myForm = this.fb.group({
      id: this.o.id,
      nomComplete: [this.o.nomComplete, Validators.required],
      email: [this.o.email, [Validators.required, Validators.email]],
      password: [this.o.password, Validators.required],
      tel: [this.o.tel, Validators.required],
      idRole: [this.o.idRole, Validators.required],
    });
  }

  resetForm() {
    this.o = new User();
    this.createForm();
  }

}
