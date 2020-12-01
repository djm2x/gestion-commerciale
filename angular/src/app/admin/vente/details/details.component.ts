import { Commande } from '../../../myModels/models';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Article } from 'src/app/myModels/models';
@Component({
  selector: 'app-update',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  o: Commande;
  title = '';
  dataSource = [];
  columnDefs = [
    { columnDef: 'article', headName: '' },
    { columnDef: 'qtePrise', headName: 'QTE' },
    { columnDef: 'prixVente', headName: 'P.V' },
    { columnDef: 'remise', headName: '' },
    { columnDef: 'total', headName: '' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName;
    return e;
  });

  displayedColumns = this.columnDefs.map(e => e.columnDef);

  constructor(public dialogRef: MatDialogRef<any>, @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.o = this.data.model;
    this.dataSource = this.o.detailCmds.map(e => {
      return {
        article: e.article,
        qtePrise: e.qtePrise,
        prixVente: e.prixVente,
        remise: e.remise,
        total: e.total,
      };
    });
    // this.title = typeof(Synthese).name.toUpperCase;
    this.title = this.data.title;
    console.log(this.dataSource);
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onOkClick(o: any): void {
    // o.date = this.valideDate(o.date);
    // this.dialogRef.close({ model: o, file: this.file });
  }



}
