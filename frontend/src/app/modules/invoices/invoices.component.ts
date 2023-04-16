import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-invoices',
  templateUrl: './invoices.component.html',
  styleUrls: ['./invoices.component.scss']
})
export class InvoicesComponent implements OnInit {

  invoices = [1,2,3,4,5,6,7];

  constructor() { }

  ngOnInit(): void {
  }

}
