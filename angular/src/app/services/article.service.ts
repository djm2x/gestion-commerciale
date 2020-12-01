import { Article } from '../myModels/models';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';


@Injectable({
  providedIn: 'root'
})
export class ArticleService extends SuperService<Article> {

  constructor() {
    super('Articles');
  }

  getForCommande(startIndex, pageSize, sortBy, sortDir, idFournisseur, idCategorie, article) {
    return this.http.get(`${this.urlApi}/${this.controller}/getForCommande/${startIndex}/${pageSize}/${sortBy}/${sortDir}/${idFournisseur}/${idCategorie}/${article}`);
  }

  getHeightSell(year, idCategorie) {
    return this.http.get(`${this.urlApi}/${this.controller}/getHeightSell/${year}/${idCategorie}`);
  }


  chiffreParCategorie(year) {
    return this.http.get(`${this.urlApi}/${this.controller}/chiffreParCategorie/${year}`);
  }


  getTop50Sell(year, idCategorie) {
    return this.http.get(`${this.urlApi}/${this.controller}/getTop50Sell/${year}/${idCategorie}`);
  }

  getForExcel() {
    return this.http.get(`${this.urlApi}/${this.controller}/getForExcel`);
  }

  getState(data) {
    return this.http.post(`${this.urlApi}/${this.controller}/getState`, data);
  }

  getAndSearch(o) {
    return this.http.post(`${this.urlApi}/${this.controller}/getAndSearch`, o);
  }

  getAll(startIndex, pageSize, idCategorie, name, constructeur, idFournisseur, idSousCategorie) {
    // console.log(`${this.urlApi}/${this.controller}/getAll/${startIndex}
    // /${pageSize}/${name}/${constructeur}/${idFournisseur}/${idCategorie}/${idSousCategorie}`)
    return this.http
    .get(`${this.urlApi}/${this.controller}/getAll/${startIndex}/${pageSize}/${idCategorie}/${name}/${constructeur}/${idFournisseur}/${idSousCategorie}`);
  }

  getProducts(startIndex, pageSize, idCategorie, name) {
    return this.http.get(`${this.urlApi}/${this.controller}/getProducts/${startIndex}/${pageSize}/${idCategorie}/${name}`);
  }

  filterBy(idFournisseur, idCategorie) {
    return this.http.get(`${this.urlApi}/${this.controller}/filterBy/${idFournisseur}/${idCategorie}`);
  }

  getConst() {
    return this.http
    .get(`${this.urlApi}/${this.controller}/getConst`);
  }

  updateQte(id, qte) {
    return this.http.get(`${this.urlApi}/${this.controller}/updateQte/${id}/${qte}`);
  }

  updateDateLastBuy(id) {
    return this.http.get(`${this.urlApi}/${this.controller}/updateDateLastBuy/${id}`);
  }
}
