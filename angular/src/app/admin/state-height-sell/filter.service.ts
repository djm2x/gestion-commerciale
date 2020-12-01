import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  year = new Subject();
  idCategorie = new Subject();

  constructor() { }
}
