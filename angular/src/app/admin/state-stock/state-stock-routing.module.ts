import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StateStockComponent } from './state-stock.component';


const routes: Routes = [
  { path: 'state-stock', redirectTo: '', pathMatch: 'full'},
  { path: '', component: StateStockComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StateStockRoutingModule { }
