import { Devis, DevisArticle } from './../../../myModels/models';
import { SessionService } from 'src/app/shared';
import { FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GlobalService } from './../global.service';
import { UowService } from 'src/app/services/uow.service';
import { Component, OnInit } from '@angular/core';
import { DetailCmd, Commande, Article, Client } from 'src/app/myModels/models';
import { PdfService } from '../../pdf.service';
import { SnackBarService } from 'src/app/loader/snack-bar.service';
import { Observable, merge } from 'rxjs';
import { MatAutocompleteSelectedEvent } from '@angular/material';

@Component({
  selector: 'app-submit',
  templateUrl: './submit.component.html',
  styleUrls: ['./submit.component.scss']
})
export class SubmitComponent implements OnInit {
  // detail = new DetailCmd();
  newCommande: Commande;
  //
  numCheque = new FormControl('');
  avance = new FormControl(0);
  date = new FormControl(new Date());
  // credit = new FormControl(0);
  modePayement = new FormControl('éspece');
  o = new Client();
  myForm: FormGroup;
  //
  myAuto = new FormControl('');
  filteredOptions: Observable<any>;
  invalid = false;
  showClientDetails = false;
  //
  constructor(public uow: UowService, public service: GlobalService
    , public pdf: PdfService, private snack: SnackBarService
    , private fb: FormBuilder, private session: SessionService) { }

  ngOnInit() {
    this.createForm();
    // this.avance.valueChanges.subscribe(() => {
    //   this.credit.setValue(this.service.globalTotal - +this.avance.value);
    // });

    merge(...[ this.myAuto.valueChanges, this.tel.valueChanges, this.ice.valueChanges]).subscribe((r) => {
      // console.log(this.myAuto.value)
      this.service.client.nom = this.myAuto.value;
      this.service.client.tel = this.tel.value;
      this.service.client.ice = this.ice.value;

      // console.log(this.service.client);

    });
  }

  createForm() {
    this.myForm = this.fb.group({
      id: this.o.id,
      nom: [this.o.nom, Validators.required],
      tel: [this.o.tel],
      ice: [this.o.ice],
    });
  }

  get tel() { return this.myForm.get('tel'); }
  get ice() { return this.myForm.get('ice'); }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.o = event.option.value as any;
    this.myAuto.setValue(this.o.nom);
    // this.service.client = this.o;
    // console.log(this.service.client);
    this.createForm();
    this.showClientDetails = true;
    // this.idOrganismeEmetteur.setValue(o.id);
  }

  async submit() {
    // this.snack.notifyOk('Commande est bien enregistrer');
    // return
    // add user
    if (this.service.client.nom !== '' && this.service.client.id === 0) {
      this.service.client = await this.uow.clients.post(this.service.client).toPromise();
    }


    const c = {
      id: 0,
      total: this.service.globalTotal,
      modePayement: this.modePayement.value,
      numCheque: this.numCheque.value,
      credit: this.service.globalTotal - +this.avance.value,
      avance: this.avance.value,
      nomClient: this.service.client.nom !== '' ? this.service.client.nom : 'Client comptoir',
      idClient: this.service.client.id !== 0 ? this.service.client.id : 1,
      date: this.uow.valideDate( this.date.value),
    };

    console.log(c);
    this.newCommande = await this.uow.commandes.post(c as any).toPromise();

    const detaiCmds = this.service.panniers.map(e => {
      return {
        idArticle: e.id,
        idCommande: this.newCommande.id,
        prixVente: e.prixVente,
        qtePrise: e.qtePrise,
        remise: e.remise,
        total: e.prixTotal,
      };
    });

    this.uow.detailCmds.postRange(detaiCmds as any[]).subscribe(r => {

      const ars = this.service.panniers.map(e => Object.assign(new Article(), e) as Article);
      console.log(ars);
      this.uow.articles.updateRange(ars).subscribe(() => {


        const detai = this.service.panniers.map(e => {
          return {
            article: e,
            prixVente: e.prixVente,
            qtePrise: e.qtePrise,
            remise: e.remise,
            total: e.prixTotal,
          };
        });

        // for the sake of print pdf
        this.newCommande.detailCmds = detai as any[];

        this.snack.notifyOk('Commande est bien enregitrer');
        // console.log(this.newCommande);
        // this.pdf.generatePdf(this.newCommande);
        this.pdfA5();
        this.reset();
      });
    });
  }

  async devis() {
    // console.log(this.myAuto.value === '')
    // return
    this.service.client = await this.uow.clients.post(this.service.client).toPromise();


    const c: any = {
      id: 0,
      client: this.service.client.nom,
      date: new Date(),
      montant: this.service.globalTotal,
    };

    console.log(c);

    const d = await this.uow.deviss.post(c as any).toPromise();

    const da = this.service.panniers.map(e => {
      return {
        idArticle: e.id,
        idDevis: d.id,
        pu: e.prixVente,
        qte: e.qtePrise,
        remise: e.remise,
        total: e.prixTotal,
      };
    });

    const cm = new Commande();
    cm.total = d.montant;
    cm.nomClient = d.client;
    cm.client.nom = d.client;
    cm.date = d.date;
    cm.detailCmds = this.service.panniers.map(e => {
      return {
        article: e,
        qtePrise: e.qtePrise,
        prixVente: e.prixVente,
        remise: e.remise,
        total: e.prixTotal,
      } as any;
    });

    await this.uow.devisArticles.postRange(da as any).toPromise();
    
    this.pdf.generatePdf(cm, 'A5', this.session.user.nomComplete, true);
  }

  reset() {
    console.log('commande added successfuly');
    this.avance.setValue(0);
    this.service.paginator.page.next(false);
    this.service.panniers = [];
    this.service.globalTotal = 0;
  }

  pdfA5(isDevis = false) {
    if (this.newCommande) {
      this.pdf.generatePdf(this.newCommande, 'A5', this.session.user.nomComplete, isDevis);
    }
  }

  pdfA6(isDevis = false) {
    if (this.newCommande) {
      this.pdf.generatePdf(this.newCommande, 'A6', this.session.user.nomComplete, isDevis);
    }
  }
}
