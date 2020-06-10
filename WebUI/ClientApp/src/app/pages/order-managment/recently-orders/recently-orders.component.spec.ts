import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecentlyOrdersComponent } from './recently-orders.component';

describe('RecentlyOrdersComponent', () => {
  let component: RecentlyOrdersComponent;
  let fixture: ComponentFixture<RecentlyOrdersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecentlyOrdersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecentlyOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
