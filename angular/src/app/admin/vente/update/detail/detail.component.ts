import { DetailCmd, Commande } from './../../../../myModels/models';
import { ModifComponent } from './modif/modif.component';
import { Article } from '../../../../myModels/models';
import { Component, OnInit, ViewChild, EventEmitter, Input } from '@angular/core';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { merge, Subject } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { DeleteService } from 'src/app/layouts/delete/delete.service';
import { Fourniture } from 'src/app/myModels/models';


@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  o: Commande;
  update = new EventEmitter();
  isLoadingResults = true;
  resultsLength = 0;
  isRateLimitReached = false;

  @Input() fromParent = new Subject();

  dataSource = [];
  columnDefs = [
    { columnDef: 'titreFr', headName: 'Article' },
    { columnDef: 'prixVente', headName: '' },
    { columnDef: 'qtePrise', headName: '' },
    { columnDef: 'remise', headName: '' },
    { columnDef: 'total', headName: '' },
    { columnDef: 'option', headName: 'OPTION' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName.toUpperCase();
    return e;
  });

  @Input() reCalculteTotalForParent = new Subject();

  displayedColumns = this.columnDefs.map(e => e.columnDef);

  constructor(private uow: UowService, public dialog: MatDialog, private mydialog: DeleteService, ) { }

  ngOnInit() {
    this.fromParent.subscribe(r => {
      this.o = r as any;
      this.getPage(0, 10, 'idArticle', 'desc', this.o.id);
    })
    
    merge(...[this.sort.sortChange, this.paginator.page, this.update]).subscribe(
      r => {
        r === true ? this.paginator.pageIndex = 0 : r = r;
        !this.paginator.pageSize ? this.paginator.pageSize = 10 : r = r;
        const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.isLoadingResults = true;
        this.getPage(
          startIndex,
          this.paginator.pageSize,
          this.sort.active ? this.sort.active : 'idArticle',
          this.sort.direction ? this.sort.direction : 'desc',
          this.o.id,
        );
      }
    );
  }

  getPage(startIndex, pageSize, sortBy, sortDir, idCmd) {
    this.uow.detailCmds.getListByCommande(startIndex, pageSize, sortBy, sortDir, idCmd).subscribe(
      (r: any) => {
        console.log(r.list);
        this.dataSource = (r.list as DetailCmd[]).map(e => {
          return {
            titreFr: e.article.titreFr,
            titreAr: e.article.titreAr,
            idArticle: e.idArticle,
            idCommande: e.idCommande,
            prixVente: e.prixVente,
            qtePrise: e.qtePrise,
            remise: e.remise,
            total: e.total,
          };
        });

        const sum = this.dataSource.map(e => e.total as number).reduce((p, c) => p + c);

        this.reCalculteTotalForParent.next(sum);
        this.resultsLength = r.count;
        this.isLoadingResults = false;
      }
    );
  }

  openDialog(o: DetailCmd, text) {
    const dialogRef = this.dialog.open(ModifComponent, {
      width: '750px',
      disableClose: true,
      data: { model: o, title: text }
    });

    return dialogRef.afterClosed();
  }

  add() {
    const f = new DetailCmd();
    f.idCommande = this.o.id;
    this.openDialog(f, 'Ajouter Detail').subscribe((result: DetailCmd) => {
      if (result) {
        console.log(result);
        this.uow.detailCmds.post(result).subscribe(
          r => {
            this.uow.articles.updateQte(result.idArticle, -result.qtePrise).subscribe(e => {
              this.update.next(true);
            });
          }
        );
      }
    });
  }

  edit(oldDetail: DetailCmd) {
    this.openDialog(oldDetail, 'Modifier Detail').subscribe((newDetail: DetailCmd) => {
      if (newDetail) {
        console.log(newDetail);
        this.uow.detailCmds.update(newDetail).subscribe(
          r => {
            console.log(-newDetail.qtePrise + oldDetail.qtePrise)
            this.uow.articles.updateQte(newDetail.idArticle, +oldDetail.qtePrise - (+newDetail.qtePrise) ).subscribe(e => {

              this.update.next(true);
            });
          }
        );
      }
    });
  }

  async delete(o: DetailCmd) {
    const r = await this.mydialog.openDialog('Detail').toPromise();
    if (r === 'ok') {
      this.uow.detailCmds.remove(o.idArticle, o.idCommande).subscribe(() => {
        this.uow.articles.updateQte(o.idArticle, +o.qtePrise).subscribe(e => {
          this.update.next(true);
        });
      });
    }
  }

}





