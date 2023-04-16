import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReachedInvoiceChartComponent } from './reached-invoice-chart.component';

describe('ReachedInvoiceChartComponent', () => {
  let component: ReachedInvoiceChartComponent;
  let fixture: ComponentFixture<ReachedInvoiceChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReachedInvoiceChartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReachedInvoiceChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
