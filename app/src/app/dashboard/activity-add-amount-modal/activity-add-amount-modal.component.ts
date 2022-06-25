import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Code, Reward } from 'src/app/models/reward';
import { RewardService } from 'src/app/services/reward.service';

@Component({
  selector: 'app-activity-add-amount-modal',
  templateUrl: './activity-add-amount-modal.component.html',
  styleUrls: ['./activity-add-amount-modal.component.scss']
})
export class ActivityAddAmountModalComponent implements OnInit {
  formAmount = this.fb.group({
    amount: ['', Validators.required],
  });
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private rewardService: RewardService,
    private dialogRef: MatDialogRef<ActivityAddAmountModalComponent>,
    private router: Router
  ) { }

  ngOnInit(): void {
  }
  sendActivity() {
    let activity: Reward = {
      tripId: this.data.tripId,
      code: Code.DONATION,
      amount: +this.formAmount.value.amount!,
      distance: undefined
    }
    this.rewardService.addReward(
      activity
    ).subscribe(() => {
      this.router.navigate(['/dashboard/trips/' + this.data.tripId])
      this.dialogRef.close();
    })
  }
}
