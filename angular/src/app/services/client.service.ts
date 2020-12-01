import { Client } from '../myModels/models';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';


const url = 'http://localhost:5000/api/Clients';
@Injectable({
  providedIn: 'root'
})
export class ClientService extends SuperService<Client> {

  constructor() {
    super('Clients');
  }

  autoCompleteClient(value) {
    return this.http.get(`${this.urlApi}/${this.controller}/autoCompleteClient/${value}`);
  }
}
