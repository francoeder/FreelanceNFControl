import { Component, OnInit } from '@angular/core';

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

  constructor() { }

  ngOnInit() {
  }
}
