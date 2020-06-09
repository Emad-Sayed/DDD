import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DistributorEditorComponent } from './distributor-editor.component';

describe('DistributorEditorComponent', () => {
  let component: DistributorEditorComponent;
  let fixture: ComponentFixture<DistributorEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DistributorEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DistributorEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
