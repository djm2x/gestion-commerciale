import { Commande } from '../myModels/models';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';


@Injectable({
  providedIn: 'root'
})
export class CommandeService extends SuperService<Commande> {

  constructor() {
    super('Commandes');
  }

  
  chiffreParMois(year) {
    return this.http.get(`${this.urlApi}/${this.controller}/chiffreParMois/${year}`);
  }

  getChiffreParAnnee(year) {
    return this.http.get(`${this.urlApi}/${this.controller}/getChiffreParAnnee/${year}`);
  }

  getCreditClients() {
    return this.http.get(`${this.urlApi}/${this.controller}/getCreditClients`);
  }

  chiffreParAnnee() {
    return this.http.get(`${this.urlApi}/${this.controller}/chiffreParAnnee`);
  }

  getByIdClient(startIndex, pageSize, sortBy, sortDir, id) {
    return this.http.get(`${this.urlApi}/${this.controller}/getAll/${startIndex}/${pageSize}/${sortBy}/${sortDir}/${id}`);
  }

  getAllByDate(o) {
    return this.http
    .post(`${this.urlApi}/${this.controller}/getAllByDate/`, o);
  }

  getCreditByClient(id) {
    return this.http.get(`${this.urlApi}/${this.controller}/getCreditByClient/${id}`);
  }

  getByIds(idArticle, idFornisseur) {
    return this.http.get(`${this.urlApi}/${this.controller}/getByIds/${idArticle}/${idFornisseur}`);
  }

  autoCompleteClient(value) {
    return this.http.get(`${this.urlApi}/${this.controller}/autoCompleteClient/${value}`);
  }
}
