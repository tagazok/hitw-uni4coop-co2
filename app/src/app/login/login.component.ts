import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    private authService: AuthService
  ) {
    if (this.authService.user) {
      console.log("user exists");
    }
  }

  ngOnInit(): void {
  }

}
