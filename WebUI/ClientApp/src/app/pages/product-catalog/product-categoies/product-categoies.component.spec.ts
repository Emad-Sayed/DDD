import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCategoiesComponent } from './product-categoies.component';

describe('ProductCategoiesComponent', () => {
  let component: ProductCategoiesComponent;
  let fixture: ComponentFixture<ProductCategoiesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductCategoiesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductCategoiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
