import { DevisComponent } from './devis.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UpdateComponent } from './update/update.component';


const routes: Routes = [
  { path: 'devis', redirectTo: '', pathMatch: 'full'},
  { path: '', component: DevisComponent },
  { path: 'update/:id', component: UpdateComponent }
];

// const routes: Routes = [
//   { path: '', redirectTo: '', pathMatch: 'full' },
//   {
//     path: '', component: DevisComponent,
//     children: [
//       { path: '', redirectTo: 'list', pathMatch: 'full' },
//       { path: 'list', component: ListComponent },
//       { path: 'update/:id', component: UpdateComponent },
//     ],
//     // canActivate: [ChildRouteGuard]
//   }
// ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DevisRoutingModule { }
