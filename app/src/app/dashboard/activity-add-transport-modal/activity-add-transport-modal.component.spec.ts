import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivityAddTransportModalComponent } from './activity-add-transport-modal.component';

describe('ActivityAddTransportModalComponent', () => {
  let component: ActivityAddTransportModalComponent;
  let fixture: ComponentFixture<ActivityAddTransportModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ActivityAddTransportModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ActivityAddTransportModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
