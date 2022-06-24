import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Trip } from '../../models/trip';
import { TripService } from '../../services/trip.service';

@Component({
  selector: 'app-trip-info',
  templateUrl: './trip-info.component.html',
  styleUrls: ['./trip-info.component.scss']
})
export class TripInfoComponent implements OnInit {
  public trip: Trip | undefined;
  constructor(
    private tripService: TripService,
    private activatedRoute: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.getTrip(this.activatedRoute.snapshot.params['id']);
  }
  getTrip(id: number): void {
    this.tripService.getTrip(+id).subscribe(
      (trip) => {
        this.trip = trip;
      }
    )
  }

}
