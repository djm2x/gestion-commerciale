import { ModifComponent } from './update/detail/modif/modif.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DevisRoutingModule } from './devis-routing.module';
import { DevisComponent } from './devis.component';
import { HttpClientModule } from '@angular/common/http';
import { MatModule } from 'src/app/mat.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TitleModule } from 'src/app/layouts/title/title.module';
import { UpdateComponent } from './update/update.component';
import { DetailComponent } from './update/detail/detail.component';
import { SellComponent } from './sell/sell.component';


@NgModule({
  declarations: [
    DevisComponent,
    ModifComponent,
    UpdateComponent,
    DetailComponent,
    SellComponent,
  ],
  imports: [
    CommonModule,
    DevisRoutingModule,
    HttpClientModule,
    MatModule,
    FormsModule,
    ReactiveFormsModule,
    TitleModule,
  ],
  entryComponents: [
    ModifComponent,
    SellComponent,
  ]
})
export class DevisModule { }
