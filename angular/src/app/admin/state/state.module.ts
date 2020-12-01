import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StateRoutingModule } from './state-routing.module';
import { StateComponent } from './state.component';
import { HttpClientModule } from '@angular/common/http';
import { MatModule } from 'src/app/mat.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TitleModule } from 'src/app/layouts/title/title.module';
import { CommandeComponent } from './commande/commande.component';
import { AddAvanceComponent } from './add-avance/add-avance.component';
import { UpdateComponent } from './update/update.component';
import { DetailComponent } from './update/detail/detail.component';
import { ModifComponent } from './update/detail/modif/modif.component';


@NgModule({
  declarations: [
    StateComponent,
    CommandeComponent,
    AddAvanceComponent,
    UpdateComponent,
    DetailComponent,
    ModifComponent,
  ],
  imports: [
    CommonModule,
    StateRoutingModule,
    HttpClientModule,
    MatModule,
    FormsModule,
    ReactiveFormsModule,
    TitleModule,
  ],
  entryComponents: [
    AddAvanceComponent,
    ModifComponent,
  ]
})
export class StateModule { }
