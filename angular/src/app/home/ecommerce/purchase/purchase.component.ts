import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { Client, Article } from 'src/app/myModels/models';
import { UowService } from 'src/app/services/uow.service';
import { TabService } from '../tab.service';
import { EcommerceService } from '../ecommerce.service';
import { SnackbarService } from 'src/app/shared/snakebar.service';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.scss']
})
export class PurchaseComponent implements OnInit {
  myForm: FormGroup;
  o = new Client();
  now = new Date();
  d1 = new FormControl(this.now);
  radio = new FormControl(false);
  time = new FormControl(`09:00`);

  constructor(private fb: FormBuilder, public uow: UowService
    , public tabService: TabService, public service: EcommerceService
    , private snackBar: SnackbarService) { }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.myForm = this.fb.group({
      id: this.o.id,
      nom: [this.o.nom, Validators.required],
      prenom: [this.o.prenom],
      tel: [this.o.tel, Validators.required],
      email: [this.o.email, [Validators.required, Validators.email]],
      adresse: [this.o.adresse, Validators.required],
      ice: [this.o.ice],
    });
  }

  reset() {
    this.o = new Client();
    this.createForm();
  }

  async submit(o: Client) {
    console.log(this.d1.value);
    console.log(this.radio.value);

    const client = await this.uow.clients.post(o).toPromise();

    console.log(client);

    const commande = {
      id: 0,
      total: this.service.globalTotal,
      modePayement: 'crédit',
      numCheque: '',
      credit: this.service.globalTotal,
      avance: 0,
      nomClient: client.nom,
      idClient: client.id,
      date: this.uow.valideDate(this.d1.value),
      time: this.time.value,
    };

    console.log(commande);
    const newCommande = await this.uow.commandes.post(commande as any).toPromise();

    const detaiCmds = this.service.panniers.map(e => {
      return {
        idArticle: e.id,
        idCommande: newCommande.id,
        prixVente: e.prixVente,
        qtePrise: e.qtePrise,
        remise: e.remise,
        total: e.prixTotal,
      };
    });

    this.uow.detailCmds.postRange(detaiCmds as any[]).subscribe(r => {

      const ars = this.service.panniers.map(e => Object.assign(new Article(), e) as Article);
      // console.log(ars);
      this.uow.articles.updateRange(ars).subscribe(() => {

        this.snackBar.openSnackBar('Commande est bien enregitrer');
        this.reset();
        this.service.reset();
      });
    });
  }

}
