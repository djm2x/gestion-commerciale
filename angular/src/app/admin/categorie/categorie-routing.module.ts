import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategorieComponent } from './categorie.component';


const routes: Routes = [
  { path: 'categorie', redirectTo: '', pathMatch: 'full'},
  { path: '', component: CategorieComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategorieRoutingModule { }
