import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Code, Reward } from 'src/app/models/reward';
import { RewardService } from 'src/app/services/reward.service';

@Component({
  selector: 'app-activity-add-transport-modal',
  templateUrl: './activity-add-transport-modal.component.html',
  styleUrls: ['./activity-add-transport-modal.component.scss']
})
export class ActivityAddTransportModalComponent implements OnInit {
  formNbrKilometer = this.fb.group({
    kmQuantity: ['', Validators.required],
  });


  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private rewardService: RewardService,
    private dialogRef: MatDialogRef<ActivityAddTransportModalComponent>,
    private router: Router
  ) { }

  ngOnInit(): void {
  }
  sendActivity() {
    let activity: Reward = {
      tripId: this.data.tripId,
      code: Code.TRANSPORTATION,
      distance: +this.formNbrKilometer.value.kmQuantity!
    }
    this.rewardService.addReward(
      activity
    ).subscribe(() => {
      this.router.navigate(['/dashboard/trips/' + this.data.tripId])
      this.dialogRef.close();
    })
  }


}
