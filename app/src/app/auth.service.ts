import { Injectable } from '@angular/core';
import { User } from './models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public user?: User;

  constructor() { }

  async getUser() {
    if (!this.user) {
      const payload = await fetch('/.auth/me');
      const data = await payload.json();
      this.user = data.clientPrincipal;
    }
    return this.user;
  }

}
