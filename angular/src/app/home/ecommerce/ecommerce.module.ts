import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EcommerceRoutingModule } from './ecommerce-routing.module';
import { EcommerceComponent } from './ecommerce.component';
import { ProductComponent } from './product/product.component';
import { PanierComponent } from './panier/panier.component';
import { MatModule } from 'src/app/mat.module';
import { DetailComponent } from './detail/detail.component';
import { PurchaseComponent } from './purchase/purchase.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// import { NgxMatDatetimePickerModule, NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';

@NgModule({
  declarations: [
    EcommerceComponent,
    ProductComponent,
    PanierComponent,
    DetailComponent,
    PurchaseComponent
  ],
  imports: [
    CommonModule,
    EcommerceRoutingModule,
    MatModule,
    FormsModule,
    ReactiveFormsModule,
    // NgxMatDatetimePickerModule,
    // NgxMatTimepickerModule,
    // NgxMatNativeDateModule,
  ]
})
export class EcommerceModule { }
