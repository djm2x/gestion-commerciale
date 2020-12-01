import { Categorie, DevisArticle } from '../myModels/models';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';


@Injectable({
  providedIn: 'root'
})
export class DevisArticleService extends SuperService<DevisArticle> {

  constructor() {
    super('DevisArticles');
  }

  

  getListByDevis(startIndex, pageSize, sortBy, sortDir, id) {
    return this.http
      .get(`${this.urlApi}/${this.controller}/getListByDevis/${startIndex}/${pageSize}/${sortBy}/${sortDir}/${id}`);
  }

  update = (o) =>
    this.http.post(`${this.urlApi}/${this.controller}/update`, o)

  remove = (idArticle, idCommande) =>
    this.http.delete(`${this.urlApi}/${this.controller}/delete/${idArticle}/${idCommande}`)
}
