import { Component } from "@angular/core";
import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { TeamRequest } from "src/app/sportsManager/interfaces/team-request";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { TeamService } from "src/app/sportsManager/services/team.service";
import { UserService } from "src/app/sportsManager/services/user.service";
import { UserTeam } from "src/app/sportsManager/interfaces/user-team";
import { AddFavoriteRequest } from "src/app/sportsManager/interfaces/addfavoriterequest";
import { TeamRequestFilter } from "src/app/sportsManager/interfaces/team-request-filter";
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { TeamModifyRequest } from "src/app/sportsManager/interfaces/teammodifyrequest";
import { Router } from "@angular/router";

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
    selectedTeam: TeamModifyRequest;
    userFavoriteTeams: Array<UserTeam> = [];
    addEventReqeust: AddFavoriteRequest;
    teamRequestFilter: TeamRequestFilter;

    constructor(
        private sessionService: SessionService,
        private teamService: TeamService,
        private userService: UserService,
        private router: Router) {
        super();
        this.addEventReqeust = { UserName: '', NetFavouriteTeamsOID: [] };
        this.teamRequestFilter = { teamName: '', orderAsc: true};
        this.selectedTeam = { sportOID: 0, newName: '', oldName: '', photo: null, photoString: '', teamOID: 0, newPhoto: null };
    };

    componentOnInit() {
        this.isAdmin = this.sessionService.isAdmin();
        this.updateGrid();
        this.successMessage = null;
        this.errorMessage = null;
    }

    onSearchChange(searchValue : string ) {  
        this.teamRequestFilter.teamName = searchValue;
        this.updateGrid();
    }

    updateGrid(): void {

        this.userService.getUserFavoriteTeams(this.sessionService.getCurrentUserName()).subscribe(responseFavorite => {
            this.userFavoriteTeams = responseFavorite;

            this.teamService.getTeams(this.teamRequestFilter).subscribe(response => {
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

    reportEvents($event, team: TeamRequest) {
        this.router.navigate(['/listEventByParam', { name: team.name, item: '1' }]);
    }

    selectTeam($event, team: TeamRequest) {
        this.selectedTeam.newName = team.name;
        this.selectedTeam.oldName = team.name;
        this.selectedTeam.photo = team.photo;
        this.selectedTeam.teamOID = team.teamOID;
        this.selectedTeam.photoString = team.photoString;
        this.selectedTeam.sportOID = team.sportOID;
        this.isFormActive = true;
      }

    closeForm($event) {
        this.isFormActive = false;
        this.updateGrid();
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

    changeView($event, item: Number) {
        if (item == 1) {
            this.teamRequestFilter.orderAsc = true;
        }
        else {
            this.teamRequestFilter.orderAsc = false;
        }
        this.updateGrid();
    }
}
