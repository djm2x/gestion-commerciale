import { Component, OnInit } from '@angular/core';
import { EcommerceService, PannierClient } from '../ecommerce.service';
import { TabService } from '../tab.service';

@Component({
  selector: 'app-panier',
  templateUrl: './panier.component.html',
  styleUrls: ['./panier.component.scss']
})
export class PanierComponent implements OnInit {
  invalid: boolean;

  constructor(public service: EcommerceService, public tabService: TabService) { }

  ngOnInit() {
  }

  delete(o: PannierClient) {
    const i = this.service.panniers.findIndex(e => e.id === o.id);
    this.service.panniers.splice(i, 1);
    this.service.globalTotal -= o.prixTotal;
  }

  edit(i: number, qtePrise: number) {

    this.service.panniers[i].qtePrise = qtePrise;

    this.service.globalTotal -= this.service.panniers[i].prixTotal;
    // const s = (+prixVente * +qtePrise);
    const prixVente = this.service.panniers[i].prixVente;

    this.service.panniers[i].prixTotal = (+prixVente * +qtePrise); // * (1 - remise / 100);

    this.service.globalTotal += this.service.panniers[i].prixTotal;
  }

}
