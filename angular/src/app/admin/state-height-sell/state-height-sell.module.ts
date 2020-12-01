import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HttpClientModule } from '@angular/common/http';
import { MatModule } from 'src/app/mat.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TitleModule } from 'src/app/layouts/title/title.module';
import { StateHeightSellComponent } from './state-height-sell.component';
import { StateHeightSellRoutingModule } from './state-height-sell-routing.module';
import { PreviewComponent } from './preview/preview.component';
import { OneComponent } from './one/one.component';
import { TwoComponent } from './two/two.component';
import { ChartsModule } from 'ng2-charts';
import { ThreeComponent } from './three/three.component';
import { FourComponent } from './four/four.component';
import { ZeroComponent } from './zero/zero.component';

@NgModule({
  declarations: [
    StateHeightSellComponent, 
    PreviewComponent,
    OneComponent,
    TwoComponent,
    ThreeComponent,
    FourComponent,
    ZeroComponent,
  ],
  imports: [
    CommonModule,
    StateHeightSellRoutingModule,
    HttpClientModule,
    MatModule,
    FormsModule,
    ReactiveFormsModule,
    TitleModule,
    ChartsModule,
  ],
  entryComponents: [
    PreviewComponent
  ]
})
export class StateHeightSellModule { }
