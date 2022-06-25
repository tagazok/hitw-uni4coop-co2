import { Component, OnInit } from '@angular/core';
import { Code } from 'src/app/models/reward';
import { RewardService } from 'src/app/services/reward.service';
import { Reward } from 'src/app/models/reward';
import { ActivatedRoute } from '@angular/router';


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
    private activatedRoute: ActivatedRoute
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
        let reward: Reward = {
          code: code,
          tripId: this.tripId!,
          distance: undefined,
        }
        this.rewardService.addReward(reward).subscribe(
          (receivedReward) => {
            console.log(receivedReward)
          }
        )
        break;
      case Code.TRANSPORTATION:
        break;

    }
  }

}
