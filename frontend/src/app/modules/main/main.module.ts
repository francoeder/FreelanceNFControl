import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartModule } from 'primeng/chart';
import { MainComponent } from './main.component';
import { MainRoutingModule } from './main-routing.module';

@NgModule({
    imports: [
        CommonModule,
        ChartModule,
        MainRoutingModule
    ],
    declarations: [MainComponent]
})
export class MainModule { }
