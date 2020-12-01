import { Categorie, DetailCmd } from '../myModels/models';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';


@Injectable({
  providedIn: 'root'
})
export class DetailCmdService extends SuperService<DetailCmd> {

  constructor() {
    super('DetailCmds');
  }

  

  getListByCommande(startIndex, pageSize, sortBy, sortDir, idCmd) {
    return this.http
      .get(`${this.urlApi}/${this.controller}/getListByCommande/${startIndex}/${pageSize}/${sortBy}/${sortDir}/${idCmd}`);
  }

  update = (o) =>
    this.http.post(`${this.urlApi}/${this.controller}/update`, o)

  remove = (idArticle, idCommande) =>
    this.http.delete(`${this.urlApi}/${this.controller}/delete/${idArticle}/${idCommande}`)
}
