import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitTypeWindowComponent } from './unit-type-window.component';

describe('UnitTypeWindowComponent', () => {
  let component: UnitTypeWindowComponent;
  let fixture: ComponentFixture<UnitTypeWindowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnitTypeWindowComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnitTypeWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
