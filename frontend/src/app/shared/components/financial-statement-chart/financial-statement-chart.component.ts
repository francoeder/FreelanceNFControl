import { Component, Input, OnInit } from '@angular/core';
import { ChartsService } from 'src/app/modules/main/services/charts.service';
import { FinancialStatementService } from '../../services/financial-statement.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-financial-statement-chart',
  templateUrl: './financial-statement-chart.component.html',
  styleUrls: ['./financial-statement-chart.component.scss']
})
export class FinancialStatementChartComponent implements OnInit {

  financialStatementChartData: any;
  financialStatementChartOptions: any;

  @Input() financialStatement = [];

  constructor(
    private financialStatementService: FinancialStatementService,
    private chartsService: ChartsService,
    private messageService: MessageService,
  ) {
    this.chartsService.yearFilter.subscribe((year) => {
      this.fetchSummarizedInvoicesValue(year);
    });
  }

  ngOnInit(): void { }

  fetchSummarizedInvoicesValue(year: number) {
    this.financialStatementService.getFinancialStatement(year).subscribe({
      next: (response: any) => {
        this.financialStatement = response.monthFinancialStatementList;
        this.setFinancialStatementChart();
      },
      error: (fail) => {
        this.showError(['Ocorreu um erro ao obter os dados do gráfico de balanço anual']);
      }
    })
  }

  private setFinancialStatementChart() {
    this.financialStatementChartData = {
      labels: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
      datasets: [
        {
          label: 'Notas Fiscais',
          backgroundColor: '#22c55e',
          data: this.financialStatement.map((item: any) => item.summarizedInvoicesValue)
        },
        {
          label: 'Despesas',
          backgroundColor: '#ef4444',
          data: this.financialStatement.map((item: any) => item.summarizedExpensesValue)
        }
      ]
    };

    this.financialStatementChartOptions = {
      responsive: false,
      plugins: {
          legend: {
              labels: {
                  color: '#495057'
              }
          }
      }
    }
  }

  private showError(messages: string[]) {
		messages.forEach((message) => {
			this.messageService.add({ severity: 'error', summary: 'Erro!', detail: message, life: 5000 });
		})
	}

}
