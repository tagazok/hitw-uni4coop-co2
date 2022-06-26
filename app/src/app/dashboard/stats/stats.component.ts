import { Component, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { AuthService } from 'src/app/auth.service';
import { Profile } from 'src/app/models/profile';
import { ProfilService } from 'src/app/services/profil.service';
import { StatDetailsComponent } from 'src/app/stat-details/stat-details.component';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {
  userProfile?: Profile;

  constructor(
    private authService: AuthService,
    private profileService: ProfilService,
    private bottomSheet: MatBottomSheet
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

  openStatsDetails(stat: string) {
    this.bottomSheet.open(StatDetailsComponent, {
      data: { stat: stat },
    });
  }

}
