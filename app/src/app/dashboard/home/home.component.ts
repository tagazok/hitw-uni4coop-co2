import { Component, OnInit } from '@angular/core';
import { TripService } from 'src/app/services/trip.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public total: number = 0;
  public currentStatus: number = 0;
  public nbTrips: number = 0;
  constructor(
    private tripService: TripService
  ) { }

  ngOnInit(): void {

    this.postUser();
    this.getTrips();
  }

  postUser() {
    this.tripService.login().subscribe(() => {
      console.log("User sent!");
    });
  }

  getTrips(): void {
    this.tripService.getTrips().subscribe(
      (dash) => {
        let trips = dash.trips;
        this.total = dash.totalCo2;
        trips.forEach(
          (trip) => {
            this.currentStatus += ((trip.co2 / 100) * trip.percentage);
            this.nbTrips++;
          }
        )
      }
    )
  }

}
