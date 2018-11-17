import { Component } from "@angular/core";
import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { TeamRequest } from "src/app/sportsManager/interfaces/team-request";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { TeamService } from "src/app/sportsManager/services/team.service";

@Component({
    selector: 'app-listteams',
    templateUrl: './listteams.component.html',
    styleUrls: ['./listteams.component.css']
})

export class ListTeamsComponent extends BaseComponent {

    teams: Array<TeamRequest>;
    successMessage: string = null;
    errorMessage: string = null;
    isFormActive: boolean;
    selectedTeam: TeamRequest;

    constructor(
        private sessionService: SessionService,
        private teamService: TeamService) {
        super();
    };

    ngOnInit() {
        this.updateGrid();
        this.successMessage = null;
        this.errorMessage = null;
    }

    updateGrid(): void {
        this.teamService.getTeams().subscribe(response => {
            this.teams = response;
        });
    }

    deleteTeam($event, team: TeamRequest) {
        this.teamService.deleteTeam(team.name).subscribe(
            response => this.handleResponse(response),
            error => this.handleError(error));
    }

    selectTeam($event, team: TeamRequest) {
        this.selectedTeam = team;
        this.isFormActive = true;
      }

    closeForm($event) {
        this.isFormActive = false;
    }

    private handleResponse(response: any) {
        //this.sessionService.setSession(response);
        this.errorMessage = null;
        this.successMessage = 'Operation success';
        this.updateGrid();
      }
    
      private handleError(error: any) {
        this.successMessage = null;
        this.errorMessage = error.error;
      }

}
