import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SousCategorieRoutingModule } from './sous-categorie-routing.module';
import { SousCategorieComponent } from './sous-categorie.component';
import { HttpClientModule } from '@angular/common/http';
import { MatModule } from 'src/app/mat.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UpdateComponent } from './update/update.component';


@NgModule({
  declarations: [SousCategorieComponent, UpdateComponent],
  imports: [
    CommonModule,
    SousCategorieRoutingModule,
    HttpClientModule,
    MatModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  entryComponents: [
    UpdateComponent
  ]
})
export class SousCategorieModule { }
