import { Component, Input, Output, EventEmitter } from "@angular/core";
import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { TeamModifyRequest } from "src/app/sportsManager/interfaces/teammodifyrequest";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { TeamService } from "src/app/sportsManager/services/team.service";
import { SportService } from "src/app/sportsManager/services/sport.service";
import { SportRequest } from "src/app/sportsManager/interfaces/sportrequest";

@Component({
    selector: 'app-editteam',
    templateUrl: './editteam.component.html',
    styleUrls: ['./editteam.component.css']
  })

  export class EditTeamComponent extends BaseComponent{

    formModel: TeamModifyRequest;
    // @Input() team: TeamModifyRequest;
    _team: TeamModifyRequest;
    _oldTeamName: string; // mapear a esta propiedad el nombre viejo.
    
    get team() : TeamModifyRequest{
        return this._team; // una variable privada por ahi
      }
  
      @Input()
      set team(u: TeamModifyRequest){
          this._team = u;
      }
      
      @Output() closeRequested = new EventEmitter<boolean>();
  
      errorMessage: string = null;
      successMessage: string = null;
      sports: Array<SportRequest>;
  
      constructor(
          private sessionService: SessionService,
          private teamService: TeamService,
          private sportService: SportService) {
          super();
          this.formModel = { teamOID: 0, newName: '', oldName: '', photo: null};
        };
  
        ngOnInit() {
          this.successMessage = null;
          this.errorMessage = null;
          this.formModel.newName = this.team.newName;
          this.formModel.oldName = this.team.oldName;
          this.formModel.photo = this.team.photo;

          this.sportService.getSports().subscribe(response => {
            this.sports = response;
          });
        }  
  
        onSubmit() {
          this.teamService.editTeam(this.team)
            .subscribe(
              response => this.handleResponse(response),
              error => this.handleError(error));
        }
      
        private handleResponse(response: any) {
          //this.sessionService.setSession(response);
          this.errorMessage = null;
          this.successMessage = 'Operation success';
        }
      
        private handleError(error: any) {
          this.successMessage = null;
          this.errorMessage = error.error;
        }
        
        cancel() {
          this.closeRequested.emit(true);
        }
}