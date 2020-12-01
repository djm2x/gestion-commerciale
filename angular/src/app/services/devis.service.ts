import { Devis } from '../myModels/models';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';

@Injectable({
  providedIn: 'root'
})
export class DevisService extends SuperService<Devis> {

  constructor() {
    super('Deviss');
  }

  getAll(data) {
    return this.http.post(`${this.urlApi}/${this.controller}/getAll`, data);
  }

  getCreditByFournisseur(id) {
    return this.http.get(`${this.urlApi}/${this.controller}/getCreditByFournisseur/${id}`);
  }

  getInfoDevis(id) {
    return this.http.get(`${this.urlApi}/${this.controller}/getInfoDevis/${id}`);
  }

  
}
