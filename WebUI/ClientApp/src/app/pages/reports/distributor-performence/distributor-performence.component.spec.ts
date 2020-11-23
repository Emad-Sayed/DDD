import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DistributorPerformenceComponent } from './distributor-performence.component';

describe('DistributorPerformenceComponent', () => {
  let component: DistributorPerformenceComponent;
  let fixture: ComponentFixture<DistributorPerformenceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DistributorPerformenceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DistributorPerformenceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
