import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';

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
    isReturnTrip: ['']
  });

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
  }

}
