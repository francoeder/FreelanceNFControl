import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { TabMenuModule } from 'primeng/tabmenu';
import { MenuBarComponent } from "./menu-bar.component";

@NgModule({
    imports: [
        CommonModule,
        TabMenuModule
    ],
    exports: [ MenuBarComponent ],
    declarations: [ MenuBarComponent ],
})
export class MenuBarModule { }
