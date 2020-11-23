import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductPerformenceReportComponent } from './product-performence-report.component';

describe('ProductPerformenceReportComponent', () => {
  let component: ProductPerformenceReportComponent;
  let fixture: ComponentFixture<ProductPerformenceReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductPerformenceReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductPerformenceReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
