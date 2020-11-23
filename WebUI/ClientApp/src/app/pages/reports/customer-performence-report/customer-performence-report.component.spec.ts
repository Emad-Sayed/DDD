import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerPerformenceReportComponent } from './customer-performence-report.component';

describe('CustomerPerformenceReportComponent', () => {
  let component: CustomerPerformenceReportComponent;
  let fixture: ComponentFixture<CustomerPerformenceReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomerPerformenceReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerPerformenceReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
