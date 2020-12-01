import { Component, OnInit } from '@angular/core';
import { UowService } from 'src/app/services/uow.service';

@Component({
  selector: 'app-zero',
  templateUrl: './zero.component.html',
  styleUrls: ['./zero.component.scss']
})
export class ZeroComponent implements OnInit {
  year = new Date().getFullYear();
  getChiffreParAnnee = this.uow.commandes.getChiffreParAnnee(this.year);
  getCreditClients = this.uow.commandes.getCreditClients();
  getCreditFournisseurs = this.uow.achats.getCreditFournisseurs();

  constructor(public uow: UowService) { }

  ngOnInit() {
  }

}
