import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PannierComponent } from './pannier.component';

describe('PannierComponent', () => {
  let component: PannierComponent;
  let fixture: ComponentFixture<PannierComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PannierComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PannierComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
