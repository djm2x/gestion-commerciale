import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { Article } from 'src/app/myModels/models';

@Injectable({
  providedIn: 'root'
})
export class EcommerceService {

  // serach varibale
  public searchValue = {
    event: new Subject(),
    value: '*',
    // for detail component
    object: null,
  };

  //
  panniers: PannierClient[] = [];
  globalTotal = 0;

  constructor() { }

  reset() {
    this.panniers = [];
    this.globalTotal = 0;
  }


}

export class PannierClient extends Article {
  qtePrise = 0;
  prixTotal = 0;
  prixVente = 0;
  remise = 0;
}

