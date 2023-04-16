import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { MessageService } from 'primeng/api';
import { CustomerService } from '../../services/customer.service';
import { ValidationHelper } from '../../helpers/validation-helper';
import { ExpenseService } from '../../services/expense.service';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-expense-modal',
  templateUrl: './expense-modal.component.html',
  styleUrls: ['./expense-modal.component.scss']
})
export class ExpenseModalComponent implements OnInit {

  @Input() display: boolean = false;

  @Output() onClose: EventEmitter<any> = new EventEmitter();

  categories: any[] = [];
  selectedCategory: any;
  filteredCategories!: any[];

  customers: any[] = [];
  selectedCustomer: any;
  filteredCustomers!: any[];

  expense: any = {};
  submitted: boolean = false;

  constructor(
    private expenseService: ExpenseService,
    private categoryService: CategoryService,
    private customerService: CustomerService,
    private messageService: MessageService,
		private loaderService: NgxUiLoaderService,
  ) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe((response) => {
      this.categories = response.data;
    });

    this.customerService.getCustomers().subscribe((response) => {
      this.customers = response.data;
    });
  }

  create() {
    this.submitted = true;
    console.log(this.expense);

    if (this.expenseFullFilled()) {
      const paymentDate = (this.expense.paymentDate as Date).toISOString();
      const competenceDate = (this.expense.competenceDate as Date).toISOString();
      const selectedCustomer = !ValidationHelper.IsNullOrWhiteSpace(this.selectedCustomer) ?
        this.selectedCustomer.displayName : '';

      this.loaderService.start();

      this.expenseService.create(
        this.selectedCategory.name,
        this.expense.value,
        this.expense.description,
        paymentDate,
        competenceDate,
        selectedCustomer
      ).subscribe({
        next: (response:any) => {
          console.log(response);
          this.showSuccess("Despesa cadastrada com sucesso!");
          this.loaderService.stop();
          setTimeout(() => {
            this.onCloseEventEmit();
          }, 500);
        },
        error: (fail: any) => {
          console.log(fail);
          this.showError(["Ocorreu um erro ao tentar cadastrar a despesa"]);
          this.loaderService.stop();
        }
      });
    }
  }

  filterCategories(event: any) {
    let filtered: any[] = [];
    let query = event.query;

    for (let i = 0; i < this.categories.length; i++) {
      let category = this.categories[i];
      if (category.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
        filtered.push(category);
      }
    }

    this.filteredCategories = filtered;
  }

  filterCustomer(event: any) {
    let filtered: any[] = [];
    let query = event.query;

    for (let i = 0; i < this.customers.length; i++) {
      let customer = this.customers[i];
      if (customer.code.replace(/[^a-zA-Z0-9 ]/g, "").toLowerCase().indexOf(query.replace(/[^a-zA-Z0-9 ]/g, "").toLowerCase()) == 0) {
        filtered.push(customer);
      }
    }

    this.filteredCustomers = filtered;
  }

  onCloseEventEmit() {
    this.expense = {};
    this.selectedCategory = {};
    this.selectedCustomer = {};
    this.onClose.emit();
  }

  private expenseFullFilled(): boolean {
    return !ValidationHelper.IsNullOrWhiteSpace(this.selectedCategory) &&
           !ValidationHelper.IsNullOrWhiteSpace(this.expense.value) &&
           !ValidationHelper.IsNullOrWhiteSpace(this.expense.description) &&
           !ValidationHelper.IsNullOrWhiteSpace(this.expense.paymentDate) &&
           !ValidationHelper.IsNullOrWhiteSpace(this.expense.competenceDate);
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
