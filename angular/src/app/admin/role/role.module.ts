import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HttpClientModule } from '@angular/common/http';
import { MatModule } from 'src/app/mat.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UpdateComponent } from './update/update.component';
import { RoleComponent } from './role.component';
import { RoleRoutingModule } from './role-routing.module';


@NgModule({
  declarations: [RoleComponent, UpdateComponent],
  imports: [
    CommonModule,
    RoleRoutingModule,
    HttpClientModule,
    MatModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  entryComponents: [
    UpdateComponent
  ]
})
export class RoleModule { }
