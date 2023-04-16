import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ChartsService } from '../services/charts.service';

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.scss']
})
export class ToolBarComponent implements OnInit {

  dateYear: Date = new Date();

  @Output() displayInvoiceModal: EventEmitter<any> = new EventEmitter();
  @Output() displayExpenseModal: EventEmitter<any> = new EventEmitter();

  constructor(
    private chartsService: ChartsService
  ) { }

  ngOnInit(): void {
  }

  onYearChange() {
    this.chartsService.yearFilter.next(this.dateYear.getFullYear());
  }

  emitDisplayInvoiceModal() {
    this.displayInvoiceModal.emit();
  }

  emitDisplayExpenseModal() {
    this.displayExpenseModal.emit();
  }

}
