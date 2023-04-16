import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { DialogModule } from "primeng/dialog";
import { ExpenseModalComponent } from "./expense-modal.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ButtonModule } from "primeng/button";
import { InputTextModule } from "primeng/inputtext";
import { CurrencyMaskModule } from "ng2-currency-mask";
import { CalendarModule } from "primeng/calendar";
import { RippleModule } from "primeng/ripple";
import { AutoCompleteModule } from 'primeng/autocomplete';
import { NgxUiLoaderModule } from "ngx-ui-loader";

@NgModule({
    imports: [
        CommonModule,
        DialogModule,
        FormsModule,
        ReactiveFormsModule,
        ButtonModule,
        RippleModule,
        InputTextModule,
        CurrencyMaskModule,
        CalendarModule,
        AutoCompleteModule,
        NgxUiLoaderModule,
    ],
    exports: [ ExpenseModalComponent ],
    declarations: [ ExpenseModalComponent ],
})
export class ExpenseModalModule { }