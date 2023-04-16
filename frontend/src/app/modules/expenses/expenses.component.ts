import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.scss']
})
export class ExpensesComponent implements OnInit {

  expenses = [1,2,3,4,5,6];

  constructor() { }

  ngOnInit(): void {
  }

}
