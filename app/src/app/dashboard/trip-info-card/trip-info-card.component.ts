import { Component, Input, OnInit } from '@angular/core';
import { Trip } from 'src/app/models/trip';

@Component({
  selector: 'app-trip-info-card',
  templateUrl: './trip-info-card.component.html',
  styleUrls: ['./trip-info-card.component.scss']
})
export class TripInfoCardComponent implements OnInit {
  @Input() trip?: Trip;

  constructor() { }

  ngOnInit(): void {
  }
}
