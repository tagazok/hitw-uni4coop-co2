import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';

import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Airport } from 'src/app/models/airport';
import { Trip } from 'src/app/models/trip';
import { TripService } from 'src/app/services/trip.service';

@Component({
  selector: 'app-trip-add',
  templateUrl: './trip-add.component.html',
  styleUrls: ['./trip-add.component.scss']
})
export class TripAddComponent implements OnInit {

  addTripForm = this.fb.group({
    label: ['', Validators.required],
    departure: ['', Validators.required],
    arrival: ['', Validators.required],
    isRoundTrip: [false, Validators.required]
  });
  // filteredOptions?: Observable<Airport[]>;
  // airports: Airport[] = [{ code: 'CDG', city: 'Paris', country: 'France' }];

  constructor(
    private fb: FormBuilder,
    private tripService: TripService
  ) { }

  onSubmit() {
    const trip: Trip = {
      label: this.addTripForm.value.label || "",
      departure: this.addTripForm.value.departure || "",
      arrival: this.addTripForm.value.arrival || "",
      isRoundTrip: this.addTripForm.value.isRoundTrip || false,
      percentage: 0,
      co2: 0
    };


    this.tripService.addTrip(trip).subscribe((trip: Trip) => {
      console.log(trip);
    });
  }

  ngOnInit(): void {
    // this.filteredOptions = this.addTripForm.controls.departure.valueChanges.pipe(
    //   startWith(''),
    //   map(value => (typeof value === 'string' ? value : value?.code),
    //     map(name => (name ? this._filter(name) : this.airports.slice())),
    //   );
  }

  // displayFn(airport: Airport): string {
  //   return airport && airport.code ? airport.code : '';
  // }

  // private _filter(name: string): Airport[] {
  //   const filterValue = name.toLowerCase();

  //   return this.airports.filter(option => option.code.toLowerCase().includes(filterValue));
  // }

}
