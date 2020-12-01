import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { GlobalService } from './global.service';

@Component({
  selector: 'app-dash',
  templateUrl: './dash.component.html',
  styleUrls: ['./dash.component.scss']
})
export class DashComponent implements OnInit {

  constructor(public router: Router, private route: ActivatedRoute, public service: GlobalService) { }

  ngOnInit() {
    // this.router.events.subscribe(route => {
    //   console.log(route);
    //   // if (route instanceof NavigationStart) {
    //   //   this.route = route.url;
    //   // }
    // });

    // this.router.events.pipe(
    //   filter(event => event instanceof NavigationEnd)
    // ).subscribe((event: NavigationEnd) => {
    //   console.log(event);
    // });

    this.route.params.subscribe(params => {
      // console.log(params['id']);
      const idCategorie = params['id'] as number;

      this.service.filter.idCategorie = params['id'] as number;
      this.service.filter.update.next(false);
    });

    // this.route.data.subscribe(
    //   r => {
    //     console.log(r);

    //   }
    // );
  }

}
