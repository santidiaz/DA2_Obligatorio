import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { Event } from '../../../interfaces/event'
import { SessionService } from 'src/app/sportsManager/services/session.service';
import { UserService } from 'src/app/sportsManager/services/user.service';


@Component({
    selector: 'app-user-fav-teams-events',
    templateUrl: './user-favourite-teams-events.component.html',
    styleUrls: ['./user-favourite-teams-events.component.css']
})
export class UserFavouriteTeamsEventsComponent extends BaseComponent {

    private errorMessage: any;
    events: Array<Event> = [];
    successMessage: string = null;

    constructor(private userService: UserService, private sessionService: SessionService) {
        super();
    }

    componentOnInit() {
        this.userService.getUserFavouriteTeamsEvents(this.sessionService.getCurrentUserName()).subscribe(
            response => this.handleResponse(response),
            error => this.handleError(error));
    }

    private handleResponse(response: any) {
        this.events = <Array<Event>>response;
    }

    private handleError(error: any) {
        console.log(error);
        let algo = error;
    }
}