import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { Event } from '../../../interfaces/event'
import { SessionService } from 'src/app/sportsManager/services/session.service';
import { UserService } from 'src/app/sportsManager/services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { EventService } from 'src/app/sportsManager/services/event.service';
import { SetupEventResultRequest } from 'src/app/sportsManager/interfaces/setup-event-result-request';
import { TeamPoints } from 'src/app/sportsManager/interfaces/team-points';
import { TeamResult } from 'src/app/sportsManager/interfaces/team-result';
import { TeamService } from 'src/app/sportsManager/services/team.service';


@Component({
    selector: 'app-setup-event-result',
    templateUrl: './event-result.component.html',
    styleUrls: ['./event-result.component.css']
})
export class EventResultComponent extends BaseComponent {

    displayedColumns: string[] = ['position', 'teamName', 'teamPoints'];
    tableDataSource: Array<TeamResult> = [];
    currentEvent: Event;
    isMultipleTeamsEvent: boolean = false;
    drawMatch: boolean = false;
    winnerTeam: TeamResult;
    loseTeam: TeamResult;

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
        if(!this.currentEvent.allowMultipleTeams){
            this.setupSimpleEventData();
        } else {
            this.tableDataSource = this.currentEvent.result.teamsResult;
            this.isMultipleTeamsEvent = true;
        }
    }

    private setupSimpleEventData(){
        let teamA = this.currentEvent.result.teamsResult[0];
        let teamB = this.currentEvent.result.teamsResult[1];
        if(teamA.teamPoints == teamB.teamPoints){
            this.drawMatch = true;
            this.winnerTeam = teamA;
            this.loseTeam = teamB;
        } else if (teamA.teamPoints > teamB.teamPoints){
            this.winnerTeam = teamA;
            this.loseTeam = teamB;
        } else {
            this.winnerTeam = teamB;
            this.loseTeam = teamA;
        }
    }
    private handleError(error: any) {
        console.log(error);
    }
}