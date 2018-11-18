import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { Component, Input, Output, EventEmitter } from "@angular/core";
import { Event } from '../../../interfaces/event'
import { SessionService } from "src/app/sportsManager/services/session.service";
import { SportService } from "src/app/sportsManager/services/sport.service";
import { TeamService } from "src/app/sportsManager/services/team.service";
import { TeamRequest } from "src/app/sportsManager/interfaces/team-request";
import { EventService } from "src/app/sportsManager/services/event.service";

@Component({
    selector: 'app-editevent',
    templateUrl: './editevent.component.html',
    styleUrls: ['./editevent.component.css']
  })

  export class EditEventComponent extends BaseComponent {

    _event: Event;
    teams: Array<TeamRequest>;

    @Input()
    set event(s: Event) {
      this._event.id = s.id;
      this._event.teams = s.teams;
      this._event.sportName = s.sportName;
      this._event.initialDate = s.initialDate;
    }
    get event(): Event {
      return this._event; // una variable privada por ahi
    }
  
    @Output() closeRequested = new EventEmitter<boolean>();
  
    errorMessage: string = null;
    successMessage: string = null;
  
    constructor(
      private sessionService: SessionService,
      private teamService: TeamService,
      private sportService: SportService,
      private eventService: EventService) {
      super();
      this._event = { teamsString: [], id: 0, teams: [], sportName: '', initialDate: null, allowMultipleTeams: false, comments: [], sportId: 0, result: null };
    };
  
    ngOnInit() {
      this.successMessage = null;
      this.errorMessage = null;

      this.sportService.getSportByName(this._event.sportName).subscribe(response => {
        this.teams = response.teams;
      });
    }

    onSubmit() {

        if (this._event.id == null ||
            this._event.teamsString == null ||
            this._event.teamsString.length < 2 ||
            this._event.initialDate == null) {
          this.errorMessage = 'Complete all the required information';
        }
        else {
          this.errorMessage = null;

          this.eventService.editEvent(this._event).subscribe(
            response => this.handleResponse(response),
            error => this.handleError(error));
        }
    
      }

      private handleResponse(response: any) {
        //this.sessionService.setSession(response);
        this.errorMessage = null;
        this.successMessage = 'Operation success';
        this.cancel();
      }
    
      private handleError(error: any) {
        this.successMessage = null;
        this.errorMessage = "Validate the information entered : " + error.error + " ";
      }
    
      cancel() {
        this.closeRequested.emit(true);
      }
  
  }