import { Component, OnInit } from '@angular/core';
import { SessionService } from '../shared';
import { Router, RouterOutlet } from '@angular/router';
import { UowService } from '../services/uow.service';
import { MediaService } from '../admin/media.service';
import { EcommerceService } from './ecommerce/ecommerce.service';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { Article } from '../myModels/models';
import { switchMap } from 'rxjs/operators';
import { MatAutocompleteSelectedEvent } from '@angular/material';
import { TabService } from './ecommerce/tab.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit  {

  myAuto = new FormControl('');
  filteredOptions: Observable<Article>;

  constructor(public session: SessionService, public router: Router, private uow: UowService
    , public myMedia: MediaService, private service: EcommerceService
    , public tabService: TabService) {
  }

  ngOnInit() {
    this.autoComplete();
  }

  disconnect() {
    // this.router.navigate(['/auth']);
    this.session.doSignOut();
  }

  getState(outlet: RouterOutlet) {
    return outlet.activatedRouteData.state;
  }

  search(value: string) {
    this.service.searchValue.value = value;
    this.service.searchValue.event.next(true);


    this.tabService.tabProductIndex.setValue(0);
  }

  reset() {
    this.service.searchValue.value = '*';
    this.service.searchValue.event.next(true);
  }

  autoComplete() {
    this.filteredOptions = this.myAuto.valueChanges.pipe(
      // startWith(''),
      switchMap((value: string) => value.length > 1 ? this.uow.articles.autocomplete('titreFr', value) : []),
      // map(r => r)
    );
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    const o = event.option.value as Article;
    // console.log(o);
    this.myAuto.setValue(o.titreFr);

    this.service.searchValue.object = o;
    this.service.searchValue.event.next(true);

    this.tabService.tabProductIndex.setValue(1);
    // this.update.next(true);
    // this.idOrganismeEmetteur.setValue(o.id);
  }

  selected2(o: Article): void {
    console.log(o);
    // this.myAuto.setValue(o.titreFr);
    this.myAuto.setValue('');

    this.service.searchValue.object = o;
    this.service.searchValue.event.next(true);

    this.tabService.tabEcommerceIndex.setValue(0);
    this.tabService.tabProductIndex.setValue(1);
    // this.update.next(true);
    // this.idOrganismeEmetteur.setValue(o.id);
  }

}
