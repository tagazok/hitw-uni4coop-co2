import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Reward } from '../models/reward';

@Injectable({
  providedIn: 'root'
})
export class RewardService {
  private urlAPI = '/api/rewards/';

  constructor(
    private httpClient: HttpClient
  ) { }

  public getRewards(): Observable<Reward[]> {
    return this.httpClient.get<Reward[]>(this.urlAPI);
  }
  public addReward(reward: Reward): Observable<Reward> {
    return this.httpClient.post<Reward>(this.urlAPI, reward);
  }
  public getRedeemedActivities(tripId: number): Observable<string[]> {
    return this.httpClient.get<string[]>(this.urlAPI + 'available/' + tripId);
  }
}
