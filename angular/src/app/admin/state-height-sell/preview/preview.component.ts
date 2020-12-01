import { Article } from 'src/app/myModels/models';
import { UowService } from 'src/app/services/uow.service';
import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ExcelService } from 'src/app/shared/excel.service';

@Component({
  selector: 'app-preview',
  templateUrl: './preview.component.html',
  styleUrls: ['./preview.component.scss']
})
export class PreviewComponent implements OnInit {
  title = '';
  datasource: { libelle: string, articles: Article[] }[] = [];
  length = 0;
  @ViewChild('dataHTML', {static: true}) dataHTML: ElementRef;

  constructor(public dialogRef: MatDialogRef<any>, @Inject(MAT_DIALOG_DATA) public data: any
    , private uow: UowService, private excel: ExcelService) { }

  ngOnInit() {
    this.title = this.data.title;
    this.get();
  }

  get() {
    this.uow.articles.getForExcel().subscribe(r => {
      this.datasource = r as any;
      this.length = this.datasource.length;
    })
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onOkClick(): void {
    // this.dialogRef.close();
    // console.log(e)
    // console.log(this.dataHTML.nativeElement)
    // this.excel.doExcel(this.dataHTML);
    this.excel.test106(this.datasource);
  }

}
