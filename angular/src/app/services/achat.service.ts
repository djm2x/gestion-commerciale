import { Achat } from '../myModels/models';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';

@Injectable({
  providedIn: 'root'
})
export class AchatService extends SuperService<Achat> {

  constructor() {
    super('achats');
  }

  getAchatsWithCredit(startIndex, pageSize, sortBy, sortDir, idFournisseur) {
    return this.http.get(`${this.urlApi}/${this.controller}/getAchatsWithCredit/${startIndex}/${pageSize}/${sortBy}/${sortDir}/${idFournisseur}`);
  }

  getCreditFournisseurs() {
    return this.http.get(`${this.urlApi}/${this.controller}/getCreditFournisseurs`);
  }

  getAll(data) {
    return this.http.post(`${this.urlApi}/${this.controller}/getAll`, data);
  }

  getCreditByFournisseur(id) {
    return this.http.get(`${this.urlApi}/${this.controller}/getCreditByFournisseur/${id}`);
  }

  getInfoAchat(id) {
    return this.http.get(`${this.urlApi}/${this.controller}/getInfoAchat/${id}`);
  }

  
}
