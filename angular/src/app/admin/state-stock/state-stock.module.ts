import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HttpClientModule } from '@angular/common/http';
import { MatModule } from 'src/app/mat.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TitleModule } from 'src/app/layouts/title/title.module';
import { StateStockComponent } from './state-stock.component';
import { StateStockRoutingModule } from './state-stock-routing.module';
import { PreviewComponent } from './preview/preview.component';


@NgModule({
  declarations: [StateStockComponent, PreviewComponent],
  imports: [
    CommonModule,
    StateStockRoutingModule,
    HttpClientModule,
    MatModule,
    FormsModule,
    ReactiveFormsModule,
    TitleModule,
  ],
  entryComponents: [
    PreviewComponent
  ]
})
export class StateStockModule { }
