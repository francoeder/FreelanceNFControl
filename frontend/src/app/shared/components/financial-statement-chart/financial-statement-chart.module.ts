import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartModule } from 'primeng/chart';
import { CardModule } from 'primeng/card';
import { TagModule } from 'primeng/tag';
import { FinancialStatementChartComponent } from './financial-statement-chart.component';

@NgModule({
    imports: [
        CommonModule,
        ChartModule,
        CardModule,
        TagModule,
    ],
    exports: [
        FinancialStatementChartComponent
    ],
    declarations: [FinancialStatementChartComponent]
})
export class FinancialStatementChartModule { }
