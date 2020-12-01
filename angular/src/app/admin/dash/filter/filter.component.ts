import { FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { GlobalService } from '../global.service';
import { UowService } from 'src/app/services/uow.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {
  consts = this.uow.articles.getConst();
  fourns = this.uow.fournisseurs.get();
  categories = this.uow.categories.get();

  sousCategories = [];
  //
  name = new FormControl('');
  constructeur = new FormControl('');
  idFournisseur = new FormControl(0);
  idSousCategorie = new FormControl(0);
  idCategorie = new FormControl(0);

  constructor(private service: GlobalService, private uow: UowService,  private route: ActivatedRoute) { }

  ngOnInit() {
    // this.route.params.subscribe(params => {
    //   // console.log(params['id'], 'form filter');
    //   const idCategorie = params['id'] as number;
    //   this.uow.sousCategories.getByCat(idCategorie).subscribe(r => {
    //     this.sousCategories = r as any[];
    //   });
    // });
  }

  filter() {

    if (this.isEmpty) {
      return;
    }
    this.setData();
    //
    this.service.filter.update.next(false);
  }

  selectionChange(o) {
    // console.log(o)
    this.uow.sousCategories.getByCat(o.value).subscribe(r => {
      this.sousCategories = r as any[];
    });
  }

  reset() {
    this.name.setValue('');
    this.constructeur.setValue('');
    this.idFournisseur.setValue(0);
    this.idSousCategorie.setValue(0);
    //
    this.setData();
    this.service.filter.update.next(false);
  }

  setData() {
    this.service.filter.idFournisseur = this.idFournisseur.value;
    this.service.filter.constructeur = this.constructeur.value;
    this.service.filter.name = this.name.value;
    this.service.filter.idSousCategorie = this.idSousCategorie.value;
    this.service.filter.idCategorie = this.idCategorie.value;

    // console.log(this.idFournisseur.value)
  }

  get isEmpty() {
    return this.name.value === ''
    && this.constructeur.value === ''
    && this.idFournisseur.value === 0
    && this.idSousCategorie.value === 0
    && this.idCategorie.value === 0;
  }

}
