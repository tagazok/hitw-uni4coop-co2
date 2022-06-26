import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Profile } from 'src/app/models/profile';
import { ProfilService } from 'src/app/services/profil.service';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {
  userProfile?: Profile;

  constructor(
    private authService: AuthService,
    private profileService: ProfilService
  ) { }

  ngOnInit(): void {
    this.getData();
  }

  async getData() {
    const user = await this.authService.getUser();
    this.profileService.getUserProfile((user?.userId || "")).subscribe((data) => {
      this.userProfile = data;
    });
  }

}
