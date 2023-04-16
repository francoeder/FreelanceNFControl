import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvoicesRoutingModule } from './invoices-routing.module';
import { SkeletonModule } from 'primeng/skeleton';
import { TableModule } from 'primeng/table';
import { InvoicesComponent } from './invoices.component';

@NgModule({
    imports: [
        CommonModule,
        InvoicesRoutingModule,
        SkeletonModule,
        TableModule,
    ],
    declarations: [InvoicesComponent]
})
export class InvoicesModule { }
