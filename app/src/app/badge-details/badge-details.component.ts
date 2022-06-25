import { Component, Inject, OnInit } from '@angular/core';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material/bottom-sheet';

@Component({
  selector: 'app-badge-details',
  templateUrl: './badge-details.component.html',
  styleUrls: ['./badge-details.component.scss']
})
export class BadgeDetailsComponent implements OnInit {

  constructor(
    private bottomSheetRef: MatBottomSheetRef<BadgeDetailsComponent>,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: any
  ) { }

  ngOnInit(): void {
  }

}
