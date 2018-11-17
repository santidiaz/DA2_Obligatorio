import { Component } from "@angular/core";
import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { TeamRequest } from "src/app/sportsManager/interfaces/team-request";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { TeamService } from "src/app/sportsManager/services/team.service";
import { UserService } from "src/app/sportsManager/services/user.service";
import { UserTeam } from "src/app/sportsManager/interfaces/user-team";
import { AddFavoriteRequest } from "src/app/sportsManager/interfaces/addfavoriterequest";

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
    userFavoriteTeams: Array<UserTeam>;
    addEventReqeust: AddFavoriteRequest;

    constructor(
        private sessionService: SessionService,
        private teamService: TeamService,
        private userService: UserService) {
        super();
        this.addEventReqeust = { UserName: '', NetFavouriteTeamsOID: [] };
    };

    ngOnInit() {
        this.updateGrid();
        this.successMessage = null;
        this.errorMessage = null;
    }

    updateGrid(): void {

        this.userService.getUserFavoriteTeams(this.sessionService.getCurrentUserName()).subscribe(responseFavorite => {
            this.userFavoriteTeams = responseFavorite;

            this.teamService.getTeams().subscribe(response => {

                this.userFavoriteTeams.forEach(element2 => {
                    var aux = response.filter(x => x.teamOID === element2.teamId);
                    if(aux.length != 0) aux[0].isFavorite = true;
                });

                this.teams = response;
            });
        });
    }

    deleteTeam($event, team: TeamRequest) {
        this.teamService.deleteTeam(team.name).subscribe(
            response => this.handleResponse(response, 'Delete team success'),
            error => this.handleError(error));
    }

    selectTeam($event, team: TeamRequest) {
        this.selectedTeam = team;
        this.isFormActive = true;
      }

    closeForm($event) {
        this.isFormActive = false;
    }

    private handleResponse(response: any, message: string) {
        //this.sessionService.setSession(response);
        this.errorMessage = null;
        this.successMessage = message;
        this.updateGrid();
      }
    
      private handleError(error: any) {
        this.successMessage = null;
        this.errorMessage = error.error;
      }

      addFavoriteTeam($event, team: TeamRequest) {
        
        this.addEventReqeust.UserName = this.sessionService.getCurrentUserName();
        var listFavorite = [];
        this.userFavoriteTeams.forEach(element => {
            listFavorite.push(element.teamId);
        });
        listFavorite.push(team.teamOID);
        
        this.addEventReqeust.NetFavouriteTeamsOID = listFavorite;

        this.userService.addUserFavoriteTeams(this.sessionService.getCurrentUserName(), this.addEventReqeust).subscribe(
            response => this.handleResponse(response, 'Add favorite success'),
            error => this.handleError(error));
    }

    deleteFavoriteTeam($event, team: TeamRequest) {
        this.userService.deleteUserFavoriteTeam(this.sessionService.getCurrentUserName(), team.teamOID).subscribe(
            response => this.handleResponse(response, 'Delete favorite success'),
            error => this.handleError(error));
    }
}
