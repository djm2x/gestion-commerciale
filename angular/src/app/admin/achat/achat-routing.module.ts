import { AchatComponent } from './achat.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  { path: 'achat', redirectTo: '', pathMatch: 'full' },
  {
    path: '', component: AchatComponent,
    // children: [
    //   { path: '', redirectTo: 'list', pathMatch: 'full' },
    //   { path: 'list', component: ListComponent },
    //   { path: 'update/:id', component: UpdateComponent },
    // ],
    // canActivate: [ChildRouteGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AchatRoutingModule { }
