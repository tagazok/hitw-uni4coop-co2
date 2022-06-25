import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    const userId = +this.activatedRoute.snapshot.params['userId']
    this.profileService.getUserProfile(userId).subscribe((data) => {
      this.userProfile = data;
    });
  }
}
