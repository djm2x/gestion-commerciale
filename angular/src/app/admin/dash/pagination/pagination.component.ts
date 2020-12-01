import { Component, OnInit, ViewChild } from '@angular/core';
import { GlobalService } from '../global.service';
import { MatPaginator, PageEvent } from '@angular/material';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(public service: GlobalService) { }

  ngOnInit() {
    this.paginator.page.subscribe((r: PageEvent) => {
      this.service.paginator.pageIndex = r.pageIndex;
      this.service.paginator.pageSize = r.pageSize;
      this.service.paginator.page.next(false);
      console.log('pagination changed')
    });
  }

}
