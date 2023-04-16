import { Component, Input, OnInit } from '@angular/core';
import { ChartsService } from 'src/app/modules/main/services/charts.service';
import { InvoiceService } from '../../services/invoice.service';

@Component({
  selector: 'app-reached-invoice-chart',
  templateUrl: './reached-invoice-chart.component.html',
  styleUrls: ['./reached-invoice-chart.component.scss']
})
export class ReachedInvoiceChartComponent implements OnInit {

  reachedInvoiceChartData: any;
  reachedInvoiceChartOptions: any;

  @Input() summarizedInvoicesValue: number = 0;
  @Input() annualBillingThreshold: number = 0;

  get annualLimitReached() {
    return this.summarizedInvoicesValue >= this.annualBillingThreshold;
  }

  constructor(
    private invoiceService: InvoiceService,
    private chartsService: ChartsService,
  ) {
    this.chartsService.yearFilter.subscribe((year) => {
      this.fetchSummarizedInvoicesValue(year);
    });
  }

  ngOnInit(): void {
    console.log(this.annualLimitReached);
  }

  fetchSummarizedInvoicesValue(year: number) {
    this.invoiceService.getSummarizedInvoicesValue(year).subscribe({
      next: (response: any) => {
        this.summarizedInvoicesValue = response.summarizedInvoicesValue;
        this.annualBillingThreshold = response.annualBillingThreshold;
        this.setReachedInvoiceChart();
      },
      error: (fail) => {

      }
    })
  }

  private setReachedInvoiceChart() {
    const reachedColor = this.summarizedInvoicesValue > this.annualBillingThreshold ? '#ff6384' : '#22c55e';
    const remainingColor = '#36a2eb';

    this.reachedInvoiceChartData = {
      labels: [
        'Valor Faturado em 2023',
        'Limite de Faturamento Anual'
      ],
      datasets: [
        {
          data: [this.summarizedInvoicesValue, Math.max(this.annualBillingThreshold-this.summarizedInvoicesValue, 0)],
          backgroundColor: [
            reachedColor,
            remainingColor,
          ],
          hoverBackgroundColor: [
            reachedColor,
            remainingColor,
          ]
        }
      ]
    };

    this.reachedInvoiceChartOptions = {
      responsive: true,
      plugins: {
          legend: {
              labels: {
                  color: '#495057'
              }
          }
      }
    }
  }

}
