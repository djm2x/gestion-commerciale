import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AchatRoutingModule } from './achat-routing.module';
import { AchatComponent } from './achat.component';
import { AddComponent } from './add/add.component';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { HttpClientModule } from '@angular/common/http';
import { MatModule } from 'src/app/mat.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TitleModule } from 'src/app/layouts/title/title.module';


@NgModule({
  declarations: [AchatComponent, AddComponent, ListComponent, EditComponent],
  imports: [
    CommonModule,
    AchatRoutingModule,
    HttpClientModule,
    MatModule,
    FormsModule,
    ReactiveFormsModule,
    TitleModule,
  ],
  entryComponents: [
    EditComponent
  ]
})
export class AchatModule { }
