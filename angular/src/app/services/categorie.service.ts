import { Categorie } from '../myModels/models';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';


@Injectable({
  providedIn: 'root'
})
export class CategorieService  extends SuperService<Categorie> {

  constructor() {
    super('Categories');
  }
}
