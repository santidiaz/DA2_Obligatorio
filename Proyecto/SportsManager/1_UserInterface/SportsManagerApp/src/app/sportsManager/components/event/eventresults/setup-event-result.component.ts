import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { Event } from '../../../interfaces/event'
import { SessionService } from 'src/app/sportsManager/services/session.service';
import { UserService } from 'src/app/sportsManager/services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { EventService } from 'src/app/sportsManager/services/event.service';
import { SetupEventResultRequest } from 'src/app/sportsManager/interfaces/setup-event-result-request';


@Component({
    selector: 'app-setup-event-result',
    templateUrl: './setup-event-result.component.html',
    styleUrls: ['./setup-event-result.component.css']
})
export class SetupEventResultComponent extends BaseComponent {

    // private errorMessage: any;
    winnerTeam: string;

    currentEvent: Event;
    eventResultRequest: SetupEventResultRequest;
    showSuccessMessage: boolean = false;
    disableElements: boolean = false;

    constructor(
        private eventService: EventService,
        private router: ActivatedRoute) {
        super();
    }

    componentOnInit() {
        if (this.router != null) {
            let currentEventId = this.router.snapshot.paramMap.get('Id');
            this.eventService.getEventById(Number(currentEventId)).subscribe(
                response => this.handleResponse(response),
                error => this.handleError(error));
        }
    }

    private handleResponse(response: any) {
        this.currentEvent = <Event>response;
    }

    private handleError(error: any) {
        console.log(error);
        //let algo = error;
    }


    setupSimpleEventResult($event) {

        let teamsResult = new Array<string>();
        if (this.winnerTeam == '2') {
            teamsResult.push(this.currentEvent.teams[0].name);
            teamsResult.push(this.currentEvent.teams[1].name);
            this.eventResultRequest = { teamNames: teamsResult, drawMatch: true };
        } else if (this.winnerTeam == '0') {
            teamsResult.push(this.currentEvent.teams[0].name);
            teamsResult.push(this.currentEvent.teams[1].name);
            this.eventResultRequest = { teamNames: teamsResult, drawMatch: false };
        } else {
            teamsResult.push(this.currentEvent.teams[1].name);
            teamsResult.push(this.currentEvent.teams[0].name);
            this.eventResultRequest = { teamNames: teamsResult, drawMatch: false };
        }

        this.eventService.setupEventResult(this.currentEvent.id, this.eventResultRequest).subscribe(
            response => this.handleSetupResponse(response),
            error => this.handleError(error));
    }

    private handleSetupResponse(response: any) {
        this.showSuccessMessage = true;
        this.disableElements = true;

    }


    setupMultipleEventResult($event){
        this.eventResultRequest = { teamNames: this.playersPositionsResult, drawMatch: false };
        this.eventService.setupEventResult(this.currentEvent.id, this.eventResultRequest).subscribe(
            response => this.handleSetupResponse(response),
            error => this.handleError(error));
    }

    current_selected: string;
    playersPositionsResult: Array<string> = [];
    onSelection(e, v) {
        this.current_selected = e.option.value;

        if(e.option.selected){
            this.playersPositionsResult.push(e.option.value.name);
        } else {
            const index = this.playersPositionsResult.indexOf(e.option.value.name, 0);
            if (index > -1) {
                this.playersPositionsResult.splice(index, 1);
            }
        }
    }
}