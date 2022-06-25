import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripInfoCardComponent } from './trip-info-card.component';

describe('TripInfoCardComponent', () => {
  let component: TripInfoCardComponent;
  let fixture: ComponentFixture<TripInfoCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TripInfoCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TripInfoCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
