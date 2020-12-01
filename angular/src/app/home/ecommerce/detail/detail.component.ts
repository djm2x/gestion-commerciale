import { Component, OnInit, Inject, Input, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UowService } from 'src/app/services/uow.service';
import { Article } from 'src/app/myModels/models';
import { TabService } from '../tab.service';
import { EcommerceService, PannierClient } from '../ecommerce.service';
import { SnackbarService } from 'src/app/shared/snakebar.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {
  @Input() o = new Article();
  id = 0;
  constructor(private uow: UowService, private service: EcommerceService
    , @Inject('BASE_URL') public url: string, public tabService: TabService
    , private snackBar: SnackbarService) { }

  ngOnInit() {
    // this.id = +this.route.snapshot.paramMap.get('id');
    // if (this.id !== 0) {
    //   this.uow.articles.getOne(this.id).subscribe(r => {
    //     this.o = r as any;
    //     console.log(this.o);
    //     this.createForm();
    //   });
    // }
    this.createForm();
    this.service.searchValue.event.subscribe(r => {
      if (r) {
        console.log(this.service.searchValue.object)
        this.o = this.service.searchValue.object as any;
        this.service.searchValue.object = null;
        this.createForm();
      }
    })

    console.log(this.o);
  }

  createForm() {

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
    this.snackBar.openSnackBar('Produit ajouté au panier', null);
  }

  getImage(image) {
    if (image === '') {
      return 'assets/product.png';
    }
    return `${this.url}/articles/${image}`;
  }

}
