import { Component, OnInit } from '@angular/core';
import { Code } from 'src/app/models/reward';
import { RewardService } from 'src/app/services/reward.service';
import { Reward } from 'src/app/models/reward';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ActivityAddTransportModalComponent } from '../activity-add-transport-modal/activity-add-transport-modal.component';


@Component({
  selector: 'app-activity-add',
  templateUrl: './activity-add.component.html',
  styleUrls: ['./activity-add.component.scss']
})
export class ActivityAddComponent implements OnInit {
  private tripId: number | undefined;

  public code = Code;

  constructor(
    private rewardService: RewardService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.tripId = +this.activatedRoute.snapshot.params['tripId']
  }
  sendReward(code: Code) {
    switch (code) {
      case Code.VEGGIE:
      case Code.SHOWER:
      case Code.PLASTIC:
      case Code.COMPUTER:
      case Code.THERMOSTAT:
      case Code.RECYCLING:
        let reward: Reward = {
          code: code,
          tripId: this.tripId!,
          distance: undefined,
        }
        this.rewardService.addReward(reward).subscribe(
          (receivedReward) => {
            this.router.navigate(['/dashboard/trips/' + this.tripId])
          }
        )
        break;
      case Code.TRANSPORTATION:
        this.dialog.open(ActivityAddTransportModalComponent, {
          data: {
            tripId: this.tripId!
          }
        });
        break;

    }
  }

}
