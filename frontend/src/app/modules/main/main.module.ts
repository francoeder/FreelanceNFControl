import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main.component';
import { MainRoutingModule } from './main-routing.module';
import { MenuBarModule } from 'src/app/core/menu-bar/menu-bar.module';
import { ToolBarModule } from './tool-bar/tool-bar.module';
import { InvoiceModalModule } from 'src/app/shared/components/invoice-modal/invoice-modal.module';
import { ExpenseModalModule } from 'src/app/shared/components/expense-modal/expense-modal.module';
import { ReachedInvoiceChartModule } from 'src/app/shared/components/reached-invoice-chart/reached-invoice-chart.module';
import { FinancialStatementChartModule } from 'src/app/shared/components/financial-statement-chart/financial-statement-chart.module';

@NgModule({
    imports: [
        CommonModule,
        MainRoutingModule,
        MenuBarModule,
        ToolBarModule,
        InvoiceModalModule,
        ExpenseModalModule,
        ReachedInvoiceChartModule,
        FinancialStatementChartModule
    ],
    declarations: [MainComponent]
})
export class MainModule { }
