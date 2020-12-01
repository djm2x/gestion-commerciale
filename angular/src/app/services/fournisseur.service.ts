import { Fournisseur } from '../myModels/models';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';


const url = 'http://localhost:5000/api/fournisseurs';
@Injectable({
  providedIn: 'root'
})
export class FournisseurService extends SuperService<Fournisseur> {

  constructor() {
    super('Fournisseurs');
  }
}
