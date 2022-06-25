import { Component, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { ActivatedRoute } from '@angular/router';
import { BadgeDetailsComponent } from '../badge-details/badge-details.component';
import { ProfilService } from '../services/profil.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  userProfile: any;

  constructor(
    private profileService: ProfilService,
    private activatedRoute: ActivatedRoute,
    private bottomSheet: MatBottomSheet) { }

  ngOnInit(): void {
    const userId = +this.activatedRoute.snapshot.params['userId']
    this.profileService.getUserProfile(userId).subscribe((data) => {
      this.userProfile = data;
    });
  }

  openBadgeDetails(badge: string): void {
    this.bottomSheet.open(BadgeDetailsComponent, {
      data: { badge: badge },
    });
  }
}
