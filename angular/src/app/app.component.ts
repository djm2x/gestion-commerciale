import { Component, OnInit } from '@angular/core';
import { ThemeService } from './theme.service';
import { SplashScreenService } from './shared/splash-screen.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Front';

  constructor(public theme: ThemeService, private splashScreenService: SplashScreenService) { }

  ngOnInit() {

    this.theme.changeTheme('dark-theme');

  }
}
