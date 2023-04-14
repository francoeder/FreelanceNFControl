import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ToolBarComponent } from "./tool-bar.component";
import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from "primeng/button";
import {CalendarModule} from 'primeng/calendar';
import { FormsModule } from "@angular/forms";

@NgModule({
    imports: [
        CommonModule,
        ToolbarModule,
        ButtonModule,
        FormsModule,
        CalendarModule,
    ],
    exports: [ ToolBarComponent ],
    declarations: [ ToolBarComponent ],
})
export class ToolBarModule { }
