import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { CommonModule } from '@angular/common';
import { BaseChartDirective } from 'ng2-charts';
import { ChartOptions, ChartConfiguration } from 'chart.js';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [BaseChartDirective, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  isBrowser: boolean;

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {
    this.isBrowser = isPlatformBrowser(this.platformId);
  }

  // Line diagram 1
  public lineChartData1: ChartConfiguration<'line'>['data'] = {
    labels: [
      'January',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July',
      'August',
      'September',
      'October',
      'November',
      'December'
    ],
    datasets: [
      {
        data: [ 0, 2, 1, 3, 1, 4, 5, 2, 4, 3 ],
        label: 'Year 2024',
        fill: true,
        tension: 1,
        borderColor: 'black',
        backgroundColor: 'rgba(255,0,0,0.3)'
      }
    ]
  };
  public lineChartOptions1: ChartOptions<'line'> = {
    responsive: true
  };
  public lineChartLegend1 = true;



  // pie 1
  public pieChartOptions1: ChartOptions<'pie'> = {
    responsive: true,
  };
  public pieChartLabels1 = [ 'Days sober', 'Days drinking' ];
  public pieChartDatasets1 = [ {
    data: [ 265, 100 ]
  } ];
  public pieChartLegend1 = true;
  public pieChartPlugins1 = [];



  // pie 2
  public pieChartOptions2: ChartOptions<'pie'> = {
    responsive: true,
  };
  public pieChartLabels2 = [ 'Seeing other people smoke', 'Smelling burning cigarette', 'Too much stress at work'];
  public pieChartDatasets2 = [ {
    data: [ 60, 30, 10 ]
  } ];
  public pieChartLegend2 = true;
  public pieChartPlugins2 = [];



  // bars
  public barChartLegend = true;
  public barChartPlugins = [];

  public barChartData: ChartConfiguration<'bar'>['data'] = {
    labels: [
      'January',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July',
      'August',
      'September',
      'October',
      'November',
      'December'
    ],
    datasets: [
      { data: [ 400, 200, 60, 81, 56, 20, 10, 5, 9, 12 ], label: 'Alcool' },
      { data: [ 28, 48, 12, 24, 13, 8, 6, 4, 0, 0 ], label: '420' }
    ]
  };

  public barChartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: false,
  };
}
