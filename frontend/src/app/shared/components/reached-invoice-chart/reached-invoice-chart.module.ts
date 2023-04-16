import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartModule } from 'primeng/chart';
import { CardModule } from 'primeng/card';
import { TagModule } from 'primeng/tag';
import { ReachedInvoiceChartComponent } from './reached-invoice-chart.component';

@NgModule({
    imports: [
        CommonModule,
        ChartModule,
        CardModule,
        TagModule,
    ],
    exports: [
        ReachedInvoiceChartComponent
    ],
    declarations: [ReachedInvoiceChartComponent]
})
export class ReachedInvoiceChartModule { }
