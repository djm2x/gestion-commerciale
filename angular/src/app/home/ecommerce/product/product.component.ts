import { Component, OnInit, Inject, ViewChild, EventEmitter } from '@angular/core';
import { UowService } from 'src/app/services/uow.service';
import { MatPaginator } from '@angular/material';
import { merge } from 'rxjs';
import { startWith } from 'rxjs/operators';
import { Article } from 'src/app/myModels/models';
import { FormControl } from '@angular/forms';
import { TabService } from '../tab.service';
import { EcommerceService, PannierClient } from '../ecommerce.service';
import { SnackbarService } from 'src/app/shared/snakebar.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  categories = this.uow.categories.get();
  idCategorie = new FormControl(0);

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  update = new EventEmitter();
  dataSource = [];
  resultsLength = 0;
  isLoadingResults = false;

  seletedProduct = new Article();

  constructor(private uow: UowService, @Inject('BASE_URL') public url: string
  , public tabService: TabService, private service: EcommerceService
  , private snackBar: SnackbarService) { }

  ngOnInit() {
    merge(...[this.paginator.page, this.idCategorie.valueChanges, this.update, this.service.searchValue.event])
      .pipe(startWith(null as any)).subscribe(
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
          this.idCategorie.value,
          this.service.searchValue.value,
        );
      }
    );
  }

  getPage( startIndex, pageSize, idCategorie = 0, name = '*') {
    this.isLoadingResults = true;

    this.uow.articles.getProducts(startIndex, pageSize, idCategorie, name).subscribe(
      (r: any) => {
        console.log(r.list);
        this.dataSource = r.list;
        this.resultsLength = r.count;
        this.isLoadingResults = false;
      }
    );
  }

  isNotANumber(n) {
    return isNaN(n);
  }

  add(e: Article, q: number) {
    // this.invalid = false;
    // const i = this.service.dataSource.findIndex(o => o.id === e.id);
    // this.service.dataSource[i].qteStock--;
    let p = new PannierClient();
    p = Object.assign(p, e);

    const i = this.service.panniers.findIndex(o => o.id === e.id);
    const existeInPannier = i !== -1;
    if (existeInPannier) {
      this.service.panniers[i].prixVente = e.prixUnitaire;
      this.service.panniers[i].qtePrise += +q;
      this.service.panniers[i].qteStock -= +q;
      this.service.panniers[i].prixTotal = this.service.panniers[i].prixUnitaire * this.service.panniers[i].qtePrise;
    } else {
      // console.log(+q)
      p.prixVente = e.prixUnitaire;
      p.qtePrise = +q;
      p.qteStock -= +q;
      p.prixTotal = p.prixUnitaire * +q;
      this.service.panniers.push(p);
    }

    this.service.globalTotal = this.service.panniers.map(pp => pp.prixTotal).reduce((pp, cc) => pp + cc);
    this.snackBar.openSnackBar('Nouveau ajout au pannier');
  }

  select(e) {
    this.seletedProduct = e;
    this.tabService.tabProductIndex.setValue(1);
  }

  getImage(image) {
    if (image === '') {
      return 'assets/product.png';
    }
    return `${this.url}/articles/${image}`;
  }

}
