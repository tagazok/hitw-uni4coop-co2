import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';

  constructor() {
    this.callApi();
  }

  async callApi() {
    const payload = await fetch('/api/HttpTrigger1');
    const data = await payload.text();
    console.log(data);
  }
}
