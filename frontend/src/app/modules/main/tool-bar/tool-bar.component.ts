import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.scss']
})
export class ToolBarComponent implements OnInit {

  dateYear: Date = new Date();

  @Output() displayInvoiceModal: EventEmitter<any> = new EventEmitter();
  @Output() displayExpenseModal: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  emitDisplayInvoiceModal() {
    this.displayInvoiceModal.emit();
  }

  emitDisplayExpenseModal() {
    this.displayExpenseModal.emit();
  }

}
