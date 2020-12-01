import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ArticleRoutingModule } from './article-routing.module';
import { ListComponent } from './list/list.component';
import { MatModule } from 'src/app/mat.module';
import { ArticleComponent } from './article.component';
import { UpdateComponent } from './update/update.component';
import { TitleModule } from 'src/app/layouts/title/title.module';
import { AddComponent } from './update/fourniture/add/add.component';
import { FournitureComponent } from './update/fourniture/fourniture.component';


@NgModule({
  declarations: [ListComponent, ArticleComponent, UpdateComponent, AddComponent, FournitureComponent],
  imports: [
    CommonModule,
    ArticleRoutingModule,
    MatModule,
    ReactiveFormsModule,
    FormsModule,
    TitleModule,
  ],
  entryComponents: [
    AddComponent
  ]
})
export class ArticleModule { }
