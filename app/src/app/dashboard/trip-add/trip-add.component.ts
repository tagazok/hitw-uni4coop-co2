import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Airport } from 'src/app/models/airport';
import { Trip } from 'src/app/models/trip';
import { TripService } from 'src/app/services/trip.service';

export const _filter = (opt: string[], value: string): string[] => {
  const filterValue = value.toLowerCase();

  return opt.filter(item => item.toLowerCase().includes(filterValue));
};

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
    // departure: this.fb.group({
    //   code: ['', Validators.required],
    //   city: ['', Validators.required],
    //   country: ['', Validators.required]
    // }),
    // arrival: this.fb.group({
    //   code: ['', Validators.required],
    //   city: ['', Validators.required],
    //   country: ['', Validators.required]
    // }),
    isRoundTrip: [false, Validators.required]
  });
  // filteredOptions?: Observable<Airport[]>;
  airports: Airport[] = [
    { code: 'CDG', city: 'Paris', country: 'France' },
    { code: 'BRU', city: 'Bruxelles', country: 'Belgium' }
  ];
  filteredOptionsDeparture?: Observable<Airport[]>;
  filteredOptionsArrival?: Observable<Airport[]>;

  constructor(
    private fb: FormBuilder,
    private tripService: TripService,
    private router: Router
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

    this.tripService.addTrip(trip).subscribe({
      next: (trip: Trip) => {
        this.router.navigate(['/dashboard/trips/' + trip.id]);
      },
      error: (err) => {
        this.router.navigate(['/dashboard']);
      }
    });
  }

  ngOnInit(): void {
    this.filteredOptionsDeparture = this.addTripForm.controls.departure.valueChanges.pipe(
      startWith(''),
      map((value: any) => (typeof value === 'string' ? value : value?.code)),
      map((code: string) => (code ? this._filter(code) : this.airports.slice())),
    );
    this.filteredOptionsArrival = this.addTripForm.controls.arrival.valueChanges.pipe(
      startWith(''),
      map((value: any) => (typeof value === 'string' ? value : value?.code)),
      map((code: string) => (code ? this._filter(code) : this.airports.slice())),
    );
  }

  displayFn(airport: Airport): string {
    return airport && airport.code ? airport.code : '';
  }

  private _filter(code: string): Airport[] {
    const filterValue = code.toLowerCase();

    return this.airports.filter(option => option.code.toLowerCase().includes(filterValue));
  }

}
