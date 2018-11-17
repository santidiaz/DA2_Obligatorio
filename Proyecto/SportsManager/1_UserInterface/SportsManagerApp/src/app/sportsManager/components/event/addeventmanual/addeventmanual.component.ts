import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { Component, Output, EventEmitter } from "@angular/core";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { TeamService } from "src/app/sportsManager/services/team.service";
import { SportService } from "src/app/sportsManager/services/sport.service";
import { SportRequest } from "src/app/sportsManager/interfaces/sportrequest";
import { EventRequest } from "src/app/sportsManager/interfaces/event-request";
import { Team } from "src/app/sportsManager/interfaces/team";
import { TeamRequest } from "src/app/sportsManager/interfaces/team-request";
import { TeamRequestFilter } from "src/app/sportsManager/interfaces/team-request-filter";

@Component({
    selector: 'app-addeventmanual',
    templateUrl: './addeventmanual.component.html',
    styleUrls: ['./addeventmanual.component.css']
  })
  
  export class AddEventManualComponent extends BaseComponent {

    sports: Array<SportRequest>;
    teams: Array<TeamRequest>;
    fromModel: EventRequest;
    teamRequestFilter: TeamRequestFilter;

    @Output() closeRequested = new EventEmitter<EventRequest>();

    constructor(
        private sessionService: SessionService,
        private teamService: TeamService,
        private sportService: SportService) {
        super();
        this.clearFromModel();
        this.teamRequestFilter = { teamName: '', orderAsc: true};
      };

    
  ngOnInit() {
    //this.successMessage = null;
    //this.errorMessage = null;
    this.sportService.getSports().subscribe(response => {
      this.sports = response;
    });

    this.teamService.getTeams(this.teamRequestFilter).subscribe(response => {
      this.teams = response;
    });
  }

  addEvent() {
    this.closeRequested.emit(this.fromModel);
    this.clearFromModel();
  }

  clearFromModel(){
    this.fromModel = { 
      eventDate: null,
      teamNames: null,
      sportName: null};
  }
    
    
}