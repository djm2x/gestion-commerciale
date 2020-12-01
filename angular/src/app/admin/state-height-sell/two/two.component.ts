import { Component, OnInit, EventEmitter } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Label, Color } from 'ng2-charts';
import * as moment from 'moment';
import { UowService } from 'src/app/services/uow.service';
import { SessionService } from 'src/app/shared';
import { FormControl } from '@angular/forms';
import { FilterService } from '../filter.service';
import { merge } from 'rxjs';
import { startWith } from 'rxjs/operators';

@Component({
  selector: 'app-two',
  templateUrl: './two.component.html',
  styleUrls: ['./two.component.scss']
})
export class TwoComponent implements OnInit {
  public barChartOptions: ChartOptions = {
    responsive: true,
  };
  public barChartLabels: Label[] = [];
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;
  public barChartPlugins = [];

  public barChartData: ChartDataSets[] = [
    { data: [], label: 'Chiffre affaire / categorie' },
    // { data: [], label: 'NBR DE VILLAGES' },
    // { data: [], label: 'TAUX DE L\'ELECTRIFICATION RURALE EN %' },
  ];

  public barChartColors: Color[] = [
    { backgroundColor: '#2304bf7c' },
    { backgroundColor: 'green' },
  ];
  
  year = new FormControl(2020);
  update = new EventEmitter();
  // regions = this.uow.regions.get();
  constructor(public uow: UowService, public session: SessionService
    , private filter: FilterService) { }

  ngOnInit() {
    this.filter.year.subscribe(r => {
      this.year.setValue(r);
      this.update.next(true);
    });

    merge(...[this.update]).pipe(startWith(null as any)).subscribe(
      r => {
        this.get();
      }
    );
  }

  // get(id) {
  //   this.uow.electrificationRurals.getByForeignkey(id).subscribe((r: {list: ElectrificationRural[], provinces: Province[]}) => {
  //     this.barChartLabels = r.map(e => e.axe);

  //     this.barChartData[0].data = r.map(e => e.recommandations);
  //   });
  // }

  get() {
    this.uow.articles.chiffreParCategorie(this.year.value).subscribe((r: {categorie: string, sum: number[]}[]) => {

      console.log(r);
      // this.barChartLabels = r.map(e => moment(new Date(e.date)).format('DD/MM/YYYY'));
      this.barChartLabels = r.map(e => `${e.categorie}`);

      this.barChartData[0].data = [];
      // this.barChartData[1].data = [];
      // this.barChartData[2].data = [];

      r.forEach(e => {
        this.barChartData[0].data.push(e.sum.reduce((a, b) => a + b, 0));
        // this.barChartData[1].data.push(e.nbrVillage);
        // this.barChartData[2].data.push(e.tauxElectrification);
      });
      // this.barChartData[0].data = r.map(e => e.recommandations);
    });
  }



  selectChange() {
    this.get();
  }
}
