import { Article, Fourniture } from '../myModels/models';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';


@Injectable({
  providedIn: 'root'
})
export class FournitureService extends SuperService<Fourniture> {

  constructor() {
    super('Fournitures');
  }

  getListByAchat(startIndex, pageSize, sortBy, sortDir, id) {
    return this.http
      .get(`${this.urlApi}/${this.controller}/getListByAchat/${startIndex}/${pageSize}/${sortBy}/${sortDir}/${id}`);
  }

  getByIds(idArticle, idFornisseur) {
    return this.http.get(`${this.urlApi}/${this.controller}/getByIds/${idArticle}/${idFornisseur}`);
  }

  getListByArticle(startIndex, pageSize, sortBy, sortDir, idArticle) {
    return this.http
    .get(`${this.urlApi}/${this.controller}/getListByArticle/${startIndex}/${pageSize}/${sortBy}/${sortDir}/${idArticle}`);
  }

  getList2(startIndex, pageSize, sortBy, sortDir) {
    return this.http.get(`${this.urlApi}/${this.controller}/getList2/${startIndex}/${pageSize}/${sortBy}/${sortDir}`);
  }
}
