import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  add = new EventEmitter();
  resetForm = new EventEmitter();
  constructor() { }
}
