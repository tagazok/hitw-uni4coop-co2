import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivityAddAmountModalComponent } from './activity-add-amount-modal.component';

describe('ActivityAddAmountModalComponent', () => {
  let component: ActivityAddAmountModalComponent;
  let fixture: ComponentFixture<ActivityAddAmountModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ActivityAddAmountModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ActivityAddAmountModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
