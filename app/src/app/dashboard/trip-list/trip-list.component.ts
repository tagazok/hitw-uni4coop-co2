import { Component, OnInit } from '@angular/core';
import { Trip } from 'src/app/models/trip';
import { TripService } from 'src/app/services/trip.service';

@Component({
  selector: 'app-trip-list',
  templateUrl: './trip-list.component.html',
  styleUrls: ['./trip-list.component.scss']
})
export class TripListComponent implements OnInit {
  public trips: Trip[] = [];

  constructor(
    private tripService: TripService
  ) { }

  ngOnInit(): void {
    this.getTrips();
  }

  private getTrips() {
    this.tripService.getTrips().subscribe({
      next: (trips) => {
        this.trips = trips;
      },
      error: (err) => {
        console.log(err)
      }
    });
  }

}
