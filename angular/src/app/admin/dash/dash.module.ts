import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatModule } from './../../mat.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashRoutingModule } from './dash-routing.module';
import { DashComponent } from './dash.component';
import { PannierComponent } from './pannier/pannier.component';
import { FilterComponent } from './filter/filter.component';
import { ArticleComponent } from './article/article.component';
import { PaginationComponent } from './pagination/pagination.component';
import { SubmitComponent } from './submit/submit.component';


@NgModule({
  declarations: [DashComponent, PannierComponent, FilterComponent, ArticleComponent, PaginationComponent, SubmitComponent],
  imports: [
    CommonModule,
    DashRoutingModule,
    MatModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class DashModule { }
