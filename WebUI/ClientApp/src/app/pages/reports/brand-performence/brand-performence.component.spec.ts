import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandPerformenceComponent } from './brand-performence.component';

describe('BrandPerformenceComponent', () => {
  let component: BrandPerformenceComponent;
  let fixture: ComponentFixture<BrandPerformenceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BrandPerformenceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BrandPerformenceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
