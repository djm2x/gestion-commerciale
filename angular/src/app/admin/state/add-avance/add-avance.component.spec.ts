import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAvanceComponent } from './add-avance.component';

describe('AddAvanceComponent', () => {
  let component: AddAvanceComponent;
  let fixture: ComponentFixture<AddAvanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddAvanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAvanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
