import { DetailComponent } from './update/detail/detail.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VenteRoutingModule } from './vente-routing.module';
import { VenteComponent } from './vente.component';
import { ListComponent } from './list/list.component';
import { UpdateComponent } from './update/update.component';
import { HttpClientModule } from '@angular/common/http';
import { MatModule } from 'src/app/mat.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TitleModule } from 'src/app/layouts/title/title.module';
import { DetailsComponent } from './details/details.component';
import { ModifComponent } from './update/detail/modif/modif.component';


@NgModule({
  declarations: [
    VenteComponent,
    ListComponent,
    UpdateComponent,
    DetailsComponent,
    ModifComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    VenteRoutingModule,
    HttpClientModule,
    MatModule,
    FormsModule,
    ReactiveFormsModule,
    TitleModule,
  ],
  entryComponents: [
    DetailsComponent,
    ModifComponent,
  ]
})
export class VenteModule { }
