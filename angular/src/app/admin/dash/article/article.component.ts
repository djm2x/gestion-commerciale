import { FormControl } from '@angular/forms';
import { GlobalService, Pannier } from './../global.service';
import { Component, OnInit, Inject } from '@angular/core';
import { Article } from 'src/app/myModels/models';
import { MyToastrService } from 'src/app/shared';
import { SnackbarService } from 'src/app/shared/snakebar.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {

  qteStock = new FormControl(0);
  constructor(public service: GlobalService, @Inject('BASE_URL') public url: string
  , private toast: MyToastrService, private snackBar: SnackbarService) { }

  ngOnInit() {
    this.service.getPage(0, 10);
    // console.log(this.url);
  }

  isNotANumber(n) {
    return isNaN(n);
  }

  getImage(image) {
    if (image === '') {
      return 'assets/courses.png';
    }
    return `${this.url}/articles/${image}`;
  }

  // add(e: Article) {
  //   let p = new Pannier();
  //   p = Object.assign(e, p);
  //   const i = this.service.panniers.findIndex(o => o.id === e.id);
  //   if (i !== -1) {
  //     this.service.panniers[i].qtePrise ++;
  //   }
  //   this.service.panniers.push(e);
  // }

  increase(e: Article) {
    const i = this.service.dataSource.findIndex(o => o.id === e.id);
    // this.service.dataSource[i].qteStock ++;
  }

  add(e: Article, q: number) {
    if (!q && +q === 0) {
      return;
    }

    if (+q > e.qteStock) {
      // this.invalid = true;
      return;
    }
    console.log('Nouveau ajout au pannier')
    this.toast.toastSuccess('Nouveau ajout au pannier');
    this.snackBar.openSnackBar('Nouveau ajout au pannier');
    // this.invalid = false;
    // const i = this.service.dataSource.findIndex(o => o.id === e.id);
    // this.service.dataSource[i].qteStock--;
    let p = new Pannier();
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
  }

}
