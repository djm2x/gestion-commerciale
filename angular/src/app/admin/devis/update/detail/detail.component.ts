import { DevisArticle, Devis } from './../../../../myModels/models';
import { ModifComponent } from './modif/modif.component';
import { Component, OnInit, ViewChild, EventEmitter, Input } from '@angular/core';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { merge, Subject } from 'rxjs';
import { UowService } from 'src/app/services/uow.service';
import { DeleteService } from 'src/app/layouts/delete/delete.service';


@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  o: Devis;
  update = new EventEmitter();
  isLoadingResults = true;
  resultsLength = 0;
  isRateLimitReached = false;

  @Input() fromParent = new Subject();

  dataSource = [];
  columnDefs = [
    { columnDef: 'titreFr', headName: 'Article' },
    { columnDef: 'pu', headName: '' },
    { columnDef: 'qte', headName: '' },
    { columnDef: 'remise', headName: '' },
    { columnDef: 'total', headName: '' },
    { columnDef: 'option', headName: 'OPTION' },
  ].map(e => {
    e.headName = e.headName === '' ? e.columnDef.toUpperCase() : e.headName.toUpperCase();
    return e;
  });

  displayedColumns = this.columnDefs.map(e => e.columnDef);

  constructor(private uow: UowService, public dialog: MatDialog, private mydialog: DeleteService, ) { }

  ngOnInit() {
    this.fromParent.subscribe(r => {
      this.o = r as any;
      this.getPage(0, 10, 'idArticle', 'desc', this.o.id);
    });
    
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

  getPage(startIndex, pageSize, sortBy, sortDir, id) {
    this.uow.devisArticles.getListByDevis(startIndex, pageSize, sortBy, sortDir, id).subscribe(
      (r: any) => {
        console.log(r.list);
        this.dataSource = (r.list as DevisArticle[]).map(e => {
          return {
            id: e.id,
            titreFr: e.article.titreFr,
            titreAr: e.article.titreAr,
            idArticle: e.idArticle,
            idDevis: e.idDevis,
            pu: e.pu,
            qte: e.qte,
            remise: e.remise,
            total: e.total,
          };
        });
        this.resultsLength = r.count;
        this.isLoadingResults = false;
      }
    );
  }

  openDialog(o: DevisArticle, text) {
    const dialogRef = this.dialog.open(ModifComponent, {
      width: '750px',
      disableClose: true,
      data: { model: o, title: text }
    });

    return dialogRef.afterClosed();
  }

  // add() {
  //   const f = new DevisArticle();
  //   // f.idArticle = this.o.id;
  //   this.openDialog(f, 'Ajouter sous axe').subscribe(result => {
  //     if (result) {
  //       console.log(result);
  //       this.uow.devisArticles.post(result).subscribe(
  //         r => {
  //           this.uow.articles.updateQte(result.idArticle, +result.qte).subscribe(e => {
  //             this.update.next(true);
  //           });
  //         }
  //       );
  //     }
  //   });
  // }

  edit(oldDetail: DevisArticle) {
    this.openDialog(oldDetail, 'Modifier Detail').subscribe((newDetail: DevisArticle) => {
      if (newDetail) {
        console.log(newDetail);
        this.uow.devisArticles.put(newDetail.id, newDetail).subscribe(
          r => {
            // console.log(-newDetail.qte + oldDetail.qte)
            // this.uow.articles.updateQte(newDetail.idArticle, -newDetail.qte + oldDetail.qte).subscribe(e => {

            // });
            this.update.next(true);
          }
        );
      }
    });
  }

  async delete(o: DevisArticle) {
    const r = await this.mydialog.openDialog('Detail').toPromise();
    if (r === 'ok') {
      this.uow.devisArticles.delete(o.id).subscribe(() => {
        // this.uow.articles.updateQte(o.idArticle, +o.qte).subscribe(e => {
        // });
        this.update.next(true);
      });
    }
  }

}





