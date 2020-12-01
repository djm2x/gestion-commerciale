import { Injectable } from '@angular/core';
import { FormControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class TabService {
  tabProductIndex = new FormControl(0);
  tabEcommerceIndex = new FormControl(0);
  constructor() { }
}
