import { Component, OnInit } from '@angular/core';
import { ChartsService } from './services/charts.service';

@Component({
  templateUrl: './main.component.html'
})
export class MainComponent implements OnInit {

  reachedInvoiceChartData: any;
  reachedInvoiceChartOptions: any;

  balanceChartData: any;
  balanceChartOptions: any;

  displayInvoiceModal = false;
  displayExpenseModal = false;

  constructor(
    private chartsService: ChartsService,
  ) { }

  ngOnInit() {
  }

  invoiceModalOnClose() {
    this.displayInvoiceModal=false;
    this.chartsService.yearFilter.next(new Date().getFullYear());
  }

  expenseModalOnClose() {
    this.displayExpenseModal=false;
    this.chartsService.yearFilter.next(new Date().getFullYear());
  }
}
