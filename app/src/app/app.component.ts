import { Component, OnInit } from '@angular/core';
import { AuthService } from './auth.service';
import { User } from './models/user';
import { TripService } from './services/trip.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'app';
  user?: User;
  constructor(
    private tripService: TripService,
    public authService: AuthService) {
  }

  ngOnInit(): void {
    // this.getLoggedInUser();
    this.authService.getUser();
    this.postUser();
  }

  // async getLoggedInUser() {
  //   const payload = await fetch('/.auth/me');
  //   const data = await payload.json();
  //   this.user = data.clientPrincipal;
  // }
  postUser() {
    this.tripService.login().subscribe(() => {
      console.log("User sent!");
    });
  }
}
