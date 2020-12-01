import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SuperService } from './super.service';


@Injectable({
  providedIn: 'root'
})
export class UniteService extends SuperService<any> {

  constructor() {
    super('Unites');
  }

  getJson() {
    return this.http.get<{unite: string}[]>(`../../assets/unites.json`);
  }
}
