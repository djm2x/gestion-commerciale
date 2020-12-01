import { SousCategorie } from '../myModels/models';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';
import { of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class SousCategorieService extends SuperService<SousCategorie> {

  constructor() {
    super('SousCategories');
  }


  getByCat(idCategorie) {
    if (idCategorie === 0) {
      return of([]);
    }
    return this.http.get(`${this.urlApi}/${this.controller}/getByCat/${idCategorie}`);
  }
}
