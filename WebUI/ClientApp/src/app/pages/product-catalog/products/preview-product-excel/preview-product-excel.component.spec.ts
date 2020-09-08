import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreviewProductExcelComponent } from './preview-product-excel.component';

describe('PreviewProductExcelComponent', () => {
  let component: PreviewProductExcelComponent;
  let fixture: ComponentFixture<PreviewProductExcelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PreviewProductExcelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreviewProductExcelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
