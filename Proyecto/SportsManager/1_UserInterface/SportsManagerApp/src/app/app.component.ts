import { Component } from '@angular/core';

import { SessionService } from './sportsManager/services/session.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Sports Manager';

  constructor(private _session: SessionService) { }

   get isLoggedIn():boolean {
     return this._session.isAuthenticated();
   }
}