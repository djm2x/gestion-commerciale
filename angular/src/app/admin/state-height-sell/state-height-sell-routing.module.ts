import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StateHeightSellComponent } from './state-height-sell.component';


const routes: Routes = [
  { path: 'state-height-sell', redirectTo: '', pathMatch: 'full'},
  { path: '', component: StateHeightSellComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StateHeightSellRoutingModule { }
