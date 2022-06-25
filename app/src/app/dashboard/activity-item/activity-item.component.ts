import { Component, Input, OnInit } from '@angular/core';
import { Reward } from 'src/app/models/reward';

@Component({
  selector: 'app-activity-item',
  templateUrl: './activity-item.component.html',
  styleUrls: ['./activity-item.component.scss']
})
export class ActivityItemComponent implements OnInit {
  @Input() reward?: Reward;

  constructor() { }

  ngOnInit(): void {
  }

}
