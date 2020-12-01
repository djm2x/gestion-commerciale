import { UowService } from './../../services/uow.service';
import { merge } from 'rxjs';
import { Injectable, ViewChild, EventEmitter } from '@angular/core';
import { Article, Client } from 'src/app/myModels/models';
import { MatPaginator } from '@angular/material';
import { distinctUntilChanged } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {
  dataSource: Article[] = [];
  panniers: Pannier[] = [];
  globalTotal = 0;
  client = new Client();

  resultsLength = 0;
  isLoadingResults = false;
  paginator = new MyPaginator();
  filter = new MyFilter();
  constructor(private uow: UowService) {
    //
    merge(...[this.paginator.page, this.filter.update]).pipe(/*distinctUntilChanged()*/).subscribe(
      r => {
        // console.log('merge called', r);
        // return
        r === true ? this.paginator.pageIndex = 0 : r = r;
        !this.paginator.pageSize ? this.paginator.pageSize = 10 : r = r;
        const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.isLoadingResults = true;
        this.getPage(
          startIndex,
          this.paginator.pageSize,
          this.filter.idCategorie !== 0 ? this.filter.idCategorie : 0,
          this.filter.name !== '' ? this.filter.name : '*',
          this.filter.constructeur !== '' ? this.filter.constructeur : '*',
          this.filter.idFournisseur !== 0 ? this.filter.idFournisseur : 0,
          this.filter.idSousCategorie !== 0 ? this.filter.idSousCategorie : 0,
        );
      }
    );
  }

  getPage(
    startIndex, pageSize, idCategorie = 0, name = '*', constructeur = '*', idFournisseur = 0, idSousCategorie = 0) {
    this.uow.articles.getAll(startIndex, pageSize, idCategorie, name, constructeur, idFournisseur, idSousCategorie).subscribe(
      (r: any) => {
        console.log(r.list);
        this.dataSource = r.list;
        this.resultsLength = r.count;
        this.isLoadingResults = false;
      }
    );
  }
}

export class Pannier extends Article {
  qtePrise = 0;
  prixTotal = 0;
  prixVente = 0;
  remise = 0;
}

export class MyFilter {
  update = new EventEmitter(null);
  // update: EventEmitter<any>;
  name = '*';
  constructeur = '*';
  idFournisseur = 0;
  idSousCategorie = 0;
  idCategorie = 0;
}

export class MyPaginator {
  public page = new EventEmitter();
  public pageSize = 10;
  public pageIndex = 0;
}

export class MySort {
  public sortChange = new EventEmitter();
  public active = '';
  public direction = '';
}
