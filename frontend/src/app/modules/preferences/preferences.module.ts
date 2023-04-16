import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PreferencesRoutingModule } from './preferences-routing.module';
import { SkeletonModule } from 'primeng/skeleton';
import { TableModule } from 'primeng/table';
import { PreferencesComponent } from './preferences.component';

@NgModule({
    imports: [
        CommonModule,
        PreferencesRoutingModule,
        TableModule,
        SkeletonModule,
    ],
    declarations: [PreferencesComponent]
})
export class PreferencesModule { }
