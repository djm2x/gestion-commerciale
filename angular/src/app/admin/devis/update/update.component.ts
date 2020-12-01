import { Devis } from './../../../myModels/models';
import { SessionService } from './../../../shared/session.service';
import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import { Validators, FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UowService } from 'src/app/services/uow.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class UpdateComponent implements OnInit {
  //
  // fournisseurs = this.uow.fournisseurs.get();
  // categories = this.uow.categories.get();
  // sousCategories = [{ id: 1, libelle: 'Generale' }];
  

  myForm: FormGroup;
  // myDevisForm: FormGroup;

  o = new Devis();
  toChild = new Subject();
  // commande = new Fourniture();
  id = 0;
  // qteSelected = 0;
  constructor(private route: ActivatedRoute, private router: Router,
    private uow: UowService, private fb: FormBuilder, private session: SessionService) { }

  ngOnInit() {

    this.createForm();
    // this.createDevisForm();
    this.id = +this.route.snapshot.paramMap.get('id');
    if (this.id !== 0) {
      this.uow.deviss.getOne(this.id).subscribe(r => {
        this.o = r as any;
        // this.qteStock.setValue(r.qteStock);
        console.log(this.o);
        // this.pieceJointe = this.o.image;
        this.createForm();
        this.toChild.next(this.o);
      });
    }

    // this.myDevisForm.get('qte').valueChanges.subscribe(r => {
    //   this.qteStock.setValue(r - this.qteSelected);
    // });
  }

  createForm() {
    this.myForm = this.fb.group({
      id: this.o.id,
      client: [this.o.client, Validators.required],
      montant: [this.o.montant, Validators.required],
      date: [this.o.date, Validators.required],
    });
  }

  // createDevisForm() {
  //   this.myDevisForm = this.fb.group({
  //     id: this.commande.id,
  //     idArcticle: [this.commande.idArcticle, Validators.required],
  //     idFournisseur: [this.commande.idFournisseur, Validators.required],
  //     qte: [this.commande.qte, Validators.required],
  //     prixAchat: [this.commande.prixAchat, Validators.required],
  //     dateAchat: [this.commande.dateAchat, Validators.required],
  //   });
  // }

  submit(o: Devis) {
    console.log(o);
    // return;
    if (this.id === 0) {
      this.uow.deviss.post(o).subscribe(r => {
        this.router.navigate(['/admin/devis']);
      });
    } else {
      this.uow.deviss.put(o.id, o).subscribe(r => {
        this.router.navigate(['/admin/devis']);
      });
    }
  }

}
