import { TitleComponent } from './title.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatModule } from 'src/app/mat.module';



@NgModule({
  declarations: [TitleComponent],
  imports: [
    CommonModule,
    MatModule,
  ],
  exports: [TitleComponent]
})
export class TitleModule { }
