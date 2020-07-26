import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCategoryEditorComponent } from './product-category-editor.component';

describe('ProductCategoryEditorComponent', () => {
  let component: ProductCategoryEditorComponent;
  let fixture: ComponentFixture<ProductCategoryEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductCategoryEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductCategoryEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
