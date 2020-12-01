import { UowService } from './../services/uow.service';
import { Router, NavigationStart, RouterOutlet } from '@angular/router';
import { Component, ViewChild, ChangeDetectorRef, OnInit } from '@angular/core';
import { MatButton } from '@angular/material';
import { MediaMatcher } from '@angular/cdk/layout';
import { User } from '../myModels/models';
import { SessionService } from '../shared';
import { ThemeService } from '../theme.service';
import { BreakpointObserver, Breakpoints, BreakpointState} from '@angular/cdk/layout';
import { MediaService } from './media.service';
@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
})
export class AdminComponent implements OnInit {
  @ViewChild('btndev', { static: true }) btndev: MatButton;
  @ViewChild('snav', { static: true }) snav: any;
  keyDevTools = '';
  panelOpenState = false;
  mobileQuery: MediaQueryList;
  currentSection = 'section1';
  userImg = '../../assets/caisse.jpg';
  opened = false;
  idRole = -1;
  isConnected = false;
  // montantCaisse = this.s.notify;
  route = this.router.url;
  user = new User();
  // categories = this.uow.categories.get();
  constructor(public session: SessionService, changeDetectorRef: ChangeDetectorRef
    , public media: MediaMatcher, public router: Router, private uow: UowService
    , public theme: ThemeService, public myMedia: MediaService) {


    this.mobileQuery = media.matchMedia('(max-width: 1000px)');
    this.mobileQuery.addListener((e: MediaQueryListEvent) => changeDetectorRef.detectChanges());
  }

  ngOnInit() {

    // this.myMedia.windowSizeChanged.subscribe(r => {
    //   console.log(r)
    // })


    // this.getRoute();
    setTimeout(() => {
      this.user = this.session.user;
      // console.log(this.user);
      this.snav.toggle();
    }, 300);

  }

  changeTheme(theme) {
    this.theme.changeTheme(theme)
  }

  get patchRoute() { return this.route.split('/'); }

  getRoute() {
    this.router.events.subscribe(route => {
      if (route instanceof NavigationStart) {
        this.route = route.url;
        console.log(this.route);
      }
    });
  }



  disconnect() {
    this.router.navigate(['/auth']);
    this.session.doSignOut();
  }

  getState(outlet: RouterOutlet) {
    return outlet.activatedRouteData.state;
  }
}
