import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Trip } from '../models/trip';

@Injectable({
  providedIn: 'root'
})
export class TripService {
  private urlAPI = '/api/trip/';

  constructor(
    private httpClient: HttpClient
  ) { }

  public getTrips(): Observable<Trip[]> {
    return this.httpClient.get<Trip[]>(this.urlAPI);
  }
  public getTrip(id: number): Observable<Trip> {
    return this.httpClient.get<Trip>(this.urlAPI + id + '/');
  }
  public addTrip(trip: Trip): Observable<Trip> {
    return this.httpClient.post<Trip>(this.urlAPI, trip);
  }
  
}
