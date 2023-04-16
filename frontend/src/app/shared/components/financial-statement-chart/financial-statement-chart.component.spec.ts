import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinancialStatementChartComponent } from './financial-statement-chart.component';

describe('FinancialStatementChartComponent', () => {
  let component: FinancialStatementChartComponent;
  let fixture: ComponentFixture<FinancialStatementChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FinancialStatementChartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FinancialStatementChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
