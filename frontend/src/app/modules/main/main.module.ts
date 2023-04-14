import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartModule } from 'primeng/chart';
import { MainComponent } from './main.component';
import { MainRoutingModule } from './main-routing.module';
import { MenuBarModule } from 'src/app/core/menu-bar/menu-bar.module';
import { ToolBarModule } from './tool-bar/tool-bar.module';

@NgModule({
    imports: [
        CommonModule,
        ChartModule,
        MainRoutingModule,
        MenuBarModule,
        ToolBarModule
    ],
    declarations: [MainComponent]
})
export class MainModule { }
