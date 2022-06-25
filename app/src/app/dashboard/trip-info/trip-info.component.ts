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
  public randomTip: string = "";

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
        this.generateRandomTip();
      }
    )
  }

  generateRandomTip() {
    const tips = [
      `This travel represents ${((this.trip?.co2 || 0) / 7).toFixed()} steaks of 200g`,
      `To compensate this travel, you should take ${((this.trip?.co2 || 0) / 1.06).toFixed()} showers instead of taking baths`,
      `To compensate this travel, you should reuse ${((this.trip?.co2 || 0) / 0.2).toFixed()} times a bag instead of buying a plastic bag`,
      `To compensate this travel, you should turn down your thermostat by 1°C for ${((this.trip?.co2 || 0)).toFixed()} days`,
      `To compensate this travel, you should turn off your computer ${((this.trip?.co2 || 0) / 0.2).toFixed()} days instead of using battery saver`
    ];

    this.randomTip = tips[Math.floor(Math.random() * tips.length)];
  }
}
