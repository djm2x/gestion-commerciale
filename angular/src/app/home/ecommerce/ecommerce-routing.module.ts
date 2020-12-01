import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EcommerceComponent } from './ecommerce.component';
import { ProductComponent } from './product/product.component';
import { PanierComponent } from './panier/panier.component';
import { DetailComponent } from './detail/detail.component';


const routes: Routes = [
  { path: '', redirectTo: '', pathMatch: 'full' },
  {
    path: '', component: EcommerceComponent,
    children: [
      { path: '', redirectTo: 'product', pathMatch: 'full'},
      { path: 'product', component: ProductComponent },
      { path: 'detail/:id', component: DetailComponent },
      { path: 'panier', component: PanierComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EcommerceRoutingModule { }
