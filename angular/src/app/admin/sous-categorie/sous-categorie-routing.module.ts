import { SousCategorieComponent } from './sous-categorie.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  { path: 'sous-categorie', redirectTo: '', pathMatch: 'full'},
  { path: '', component: SousCategorieComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SousCategorieRoutingModule { }
