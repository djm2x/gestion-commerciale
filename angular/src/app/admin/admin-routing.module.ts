import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';


const routes: Routes = [
  { path: '', redirectTo: '', pathMatch: 'full' },
  {
    path: '', component: AdminComponent,
    children: [
      { path: '', redirectTo: 'vente', pathMatch: 'full'},

      { path: 'article', loadChildren: () => import('./article/article.module').then(m => m.ArticleModule), },
      { path: 'vente', loadChildren: () => import('./vente/vente.module').then(m => m.VenteModule), },
      { path: 'categorie', loadChildren: () => import('./categorie/categorie.module').then(m => m.CategorieModule), },
      { path: 'sous-categorie', loadChildren: () => import('./sous-categorie/sous-categorie.module').then(m => m.SousCategorieModule), },
      { path: 'fournisseur', loadChildren: () => import('./fournisseur/fournisseur.module').then(m => m.FournisseurModule), },
      { path: 'user', loadChildren: () => import('./user/user.module').then(m => m.UserModule), },
      { path: 'achat', loadChildren: () => import('./achat/achat.module').then(m => m.AchatModule), },
      { path: 'state', loadChildren: () => import('./state/state.module').then(m => m.StateModule), },
      { path: 'state-stock', loadChildren: () => import('./state-stock/state-stock.module').then(m => m.StateStockModule), },
      { path: 'devis', loadChildren: () => import('./devis/devis.module').then(m => m.DevisModule), },
      { path: 'state-height-sell', loadChildren: () => import('./state-height-sell/state-height-sell.module')
      .then(m => m.StateHeightSellModule), },
      { path: 'role', loadChildren: () => import('./role/role.module').then(m => m.RoleModule), },
      // { path: 'state-client', loadChildren: () => import('./state-client/state-client.module').then(m => m.StateClientModule), },
      { path: 'dash', loadChildren: () => import('./dash/dash.module').then(m => m.DashModule), },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
