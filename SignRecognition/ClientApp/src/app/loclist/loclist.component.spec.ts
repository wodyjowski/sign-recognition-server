import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoclistComponent } from './loclist.component';

describe('LoclistComponent', () => {
  let component: LoclistComponent;
  let fixture: ComponentFixture<LoclistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoclistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoclistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
