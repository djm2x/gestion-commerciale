import { Commande, Achat } from './../../../myModels/models';
import { SessionService } from './../../../shared/session.service';
import { Article, Fourniture } from '../../../myModels/models';
import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import { Validators, FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UowService } from 'src/app/services/uow.service';
import { Subject } from 'rxjs';
import { SnackBarService } from 'src/app/loader/snack-bar.service';

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
  // myCommandeForm: FormGroup;
  o = new Achat();
  toChild = new Subject();
  // commande = new Fourniture();
  id = 0;
  //
  reCalculteTotalFromCHild = new Subject();
  // qteSelected = 0;
  constructor(private route: ActivatedRoute, private router: Router,
    public uow: UowService, private fb: FormBuilder, private snack: SnackBarService) { }

  ngOnInit() {

    this.createForm();
    // this.createCommandeForm();
    this.id = +this.route.snapshot.paramMap.get('id');
    if (this.id !== 0) {
      this.uow.achats.getOne(this.id).subscribe(r => {
        this.o = r as any;
        // this.qteStock.setValue(r.qteStock);
        console.log(this.o);
        // this.pieceJointe = this.o.image;
        this.createForm();
        this.toChild.next(this.o);
      });
    }

    this.reCalculteTotalFromCHild.subscribe(async (r: number) => {
      const t = this.myForm.get('montant').value;

      if (t !== r) {
        this.o.montant = r;
        const d = this.o.montant - this.o.avance;
        this.o.credit = d >= 0 ? d : 0;
        // this.o.montant = r;
        console.log(this.o)
        await this.uow.achats.put(this.o.id, this.o).toPromise();
        this.createForm();
      }
    });

    // this.myCommandeForm.get('qte').valueChanges.subscribe(r => {
    //   this.qteStock.setValue(r - this.qteSelected);
    // });
  }

  createForm() {
    this.myForm = this.fb.group({
      id: this.o.id,
      montant: [this.o.montant, Validators.required],
      credit: [this.o.credit, Validators.required],
      avance: [this.o.avance, Validators.required],
      idFournisseur: [this.o.idFournisseur, Validators.required],
      modePayement: [this.o.modePayement, Validators.required],
      date: [this.o.date, Validators.required],
    });
  }

  // createCommandeForm() {
  //   this.myCommandeForm = this.fb.group({
  //     id: this.commande.id,
  //     idArcticle: [this.commande.idArcticle, Validators.required],
  //     idFournisseur: [this.commande.idFournisseur, Validators.required],
  //     qte: [this.commande.qte, Validators.required],
  //     prixAchat: [this.commande.prixAchat, Validators.required],
  //     dateAchat: [this.commande.dateAchat, Validators.required],
  //   });
  // }

  submit(o: Achat) {
    console.log(o);
    // return;
    if (this.id === 0) {
      this.uow.achats.post(o).subscribe(r => {
        // this.router.navigate(['/admin/article/list']);
        this.snack.notifyOk('détail achat a été bien ajouter');

      });
    } else {
      this.uow.achats.put(o.id, o).subscribe(r => {
        // this.router.navigate(['/admin/article/list']);
        this.snack.notifyOk('détail achat a été bien modifier');
      });
    }
  }

}
