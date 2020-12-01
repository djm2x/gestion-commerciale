import { Article } from '../../../../myModels/models';
import { AddComponent } from './add/add.component';
import { Component, OnInit, ViewChild, EventEmitter, Input } from '@angular/core';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { merge } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { DeleteService } from 'src/app/layouts/delete/delete.service';
import { Fourniture } from 'src/app/myModels/models';
@Component({
  selector: 'app-fourniture',
  templateUrl: './fourniture.component.html',
  styleUrls: ['./fourniture.component.scss']
})
export class FournitureComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @Input() o: Article;
  update = new EventEmitter();
  isLoadingResults = true;
  resultsLength = 0;
  isRateLimitReached = false;

  dataSource = [];
  columnDefs = [
    { columnDef: 'fournisseur', headName: '' },
    { columnDef: 'qte', headName: '' },
    { columnDef: 'prixAchat', headName: 'PRIX ACHAT' },
    { columnDef: 'dateAchat', headName: 'DATE ACHAT' },
    { columnDef: 'option', headName: 'OPTION' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName;
    return e;
  });

  displayedColumns = this.columnDefs.map(e => e.columnDef);

  constructor(private uow: UowService, public dialog: MatDialog, private mydialog: DeleteService, ) { }

  ngOnInit() {
    this.getPage(0, 10, 'id', 'desc', this.o.id);
    merge(...[this.sort.sortChange, this.paginator.page, this.update]).subscribe(
      r => {
        r === true ? this.paginator.pageIndex = 0 : r = r;
        !this.paginator.pageSize ? this.paginator.pageSize = 10 : r = r;
        const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.isLoadingResults = true;
        this.getPage(
          startIndex,
          this.paginator.pageSize,
          this.sort.active ? this.sort.active : 'id',
          this.sort.direction ? this.sort.direction : 'desc',
          this.o.id,
        );
      }
    );
  }

  getPage(startIndex, pageSize, sortBy, sortDir, idArticle) {
    this.uow.fournitures.getListByArticle(startIndex, pageSize, sortBy, sortDir, idArticle).subscribe(
      (r: any) => {
        console.log(r.list);
        this.dataSource = r.list;
        this.resultsLength = r.count;
        this.isLoadingResults = false;
      }
    );
  }

  openDialog(o: Fourniture, text) {
    const dialogRef = this.dialog.open(AddComponent, {
      width: '750px',
      disableClose: true,
      data: { model: o, title: text }
    });

    return dialogRef.afterClosed();
  }

  add() {
    const f = new Fourniture();
    f.idArticle = this.o.id;
    this.openDialog(f, 'Ajouter achat').subscribe(result => {
      if (result) {
        console.log(result);
        this.update.next(true)
      }
    });
  }

  edit(o: Fourniture) {
    this.openDialog(o, 'Modifier achat').subscribe((result: Fourniture) => {
      if (result) {
        console.log(result);
        this.update.next(true)
      }
    });
  }

  async delete(o: Fourniture) {
    const r = await this.mydialog.openDialog('sous-axe').toPromise();
    if (r === 'ok') {
      this.uow.fournitures.delete(o.id).subscribe(() => {
        this.uow.articles.updateQte(o.idArticle, -o.qte).subscribe(e => {
          this.update.next(true);
        });
      });
    }
  }

}




