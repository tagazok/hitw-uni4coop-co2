import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Profile } from '../models/profile';

@Injectable({
  providedIn: 'root'
})
export class ProfilService {
  private urlAPI = '/api/users/';

  constructor(
    private httpClient: HttpClient
  ) { }

  public getUserProfile(id: string): Observable<Profile> {
    return this.httpClient.get<Profile>(`${this.urlAPI}${id}`);
  }
}
