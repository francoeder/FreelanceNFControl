import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpensesRoutingModule } from './expenses-routing.module';
import { SkeletonModule } from 'primeng/skeleton';
import { TableModule } from 'primeng/table';
import { ExpensesComponent } from './expenses.component';

@NgModule({
    imports: [
        CommonModule,
        ExpensesRoutingModule,
        TableModule,
        SkeletonModule,
    ],
    declarations: [ExpensesComponent]
})
export class ExpensesModule { }
