import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MessageService } from 'primeng/api';
import { InvoiceService } from '../../services/invoice.service';
import { CustomerService } from '../../services/customer.service';
import { ValidationHelper } from '../../helpers/validation-helper';

@Component({
  selector: 'app-invoice-modal',
  templateUrl: './invoice-modal.component.html',
  styleUrls: ['./invoice-modal.component.scss']
})
export class InvoiceModalComponent implements OnInit {

  @Input() display: boolean = false;

  @Output() onClose: EventEmitter<any> = new EventEmitter();

  customers: any[] = [];
  selectedCustomer: any;
  filteredCustomers!: any[];

  invoice: any = {};
  submitted: boolean = false;

  constructor(
    private invoiceService: InvoiceService,
    private customerService: CustomerService,
    private messageService: MessageService,
		private loaderService: NgxUiLoaderService,
  ) { }

  ngOnInit(): void {
    this.customerService.getCustomers().subscribe((response) => {
      this.customers = response.data;
    });
  }

  create() {
    this.submitted = true;

    if (this.invoiceFullFilled()) {
      const invoiceNumber = ("000000000" + this.invoice.invoiceNumber).slice(-9);
      const month = (this.invoice.month as Date).getMonth()+1;
      const paymentDate = (this.invoice.paymentDate as Date).toISOString();

      this.loaderService.start();

      this.invoiceService.create(this.selectedCustomer.displayName, invoiceNumber, this.invoice.value,
        this.invoice.description, month, paymentDate).subscribe({
        next: (response:any) => {
          console.log(response);
          this.showSuccess("Nota fiscal cadastrada com sucesso!");
          this.loaderService.stop();
          setTimeout(() => {
            this.onCloseEventEmit();
          }, 500);
        },
        error: (fail: any) => {
          console.log(fail);
          this.showError(["Ocorreu um erro ao tentar cadastrar a nota fiscal"]);
          this.loaderService.stop();
        }
      });
    }
  }

  filterCustomer(event: any) {
    let filtered: any[] = [];
    let query = event.query;

    for (let i = 0; i < this.customers.length; i++) {
      let country = this.customers[i];
      if (country.code.replace(/[^a-zA-Z0-9 ]/g, "").toLowerCase().indexOf(query.replace(/[^a-zA-Z0-9 ]/g, "").toLowerCase()) == 0) {
        filtered.push(country);
      }
    }

    this.filteredCustomers = filtered;
  }

  onCloseEventEmit() {
    this.invoice = {};
    this.selectedCustomer = {};
    this.onClose.emit();
  }

  private invoiceFullFilled(): boolean {
    return !ValidationHelper.IsNullOrWhiteSpace(this.selectedCustomer) &&
           !ValidationHelper.IsNullOrWhiteSpace(this.invoice.invoiceNumber) &&
           !ValidationHelper.IsNullOrWhiteSpace(this.invoice.value) &&
           !ValidationHelper.IsNullOrWhiteSpace(this.invoice.description) &&
           !ValidationHelper.IsNullOrWhiteSpace(this.invoice.month) &&
           !ValidationHelper.IsNullOrWhiteSpace(this.invoice.paymentDate);
  }

  private showError(messages: string[]) {
		messages.forEach((message) => {
			this.messageService.add({ severity: 'error', summary: 'Erro!', detail: message, life: 5000 });
		})
	}

	private showSuccess(message: string) {
    setTimeout(() => {
      this.messageService.add({severity: 'success', summary: 'Sucesso!', detail: message, life: 5000});  
    }, 750);
	}

}
