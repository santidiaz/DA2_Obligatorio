import { Component } from "@angular/core";
import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { SportRequest } from "src/app/sportsManager/interfaces/sportrequest";
import { SportService } from "src/app/sportsManager/services/sport.service";
import { SportModifyRequest } from "src/app/sportsManager/interfaces/sportmodifyrequest";
import { Router } from "@angular/router";


@Component({
    selector: 'app-listsports',
    templateUrl: './listsports.component.html',
    styleUrls: ['./listsports.component.css']
})


export class ListSportsComponent extends BaseComponent {

    sports: Array<SportRequest>;
    successMessage: string = null;
    errorMessage: string = null;
    isFormActive: boolean;
    selectedSport: SportModifyRequest;

    constructor(
        private sessionService: SessionService,
        private sportService: SportService,
        private router: Router) {
        super();
        this.selectedSport = { sportOID: 0, newName: '', oldName: '', multipleTeamsAllowed: false };
    };

    componentOnInit() {
        this.isAdmin = this.sessionService.isAdmin();
        this.updateGrid();
        this.successMessage = null;
        this.errorMessage = null;
    }

    updateGrid(): void {
        this.sportService.getSports().subscribe(response => {
            this.sports = response;
        });
    }

    deleteSport($event, sport: SportRequest) {
        this.sportService.deleteSport(sport.name).subscribe(
            response => this.handleResponse(response, 'Delete team success'),
            error => this.handleError(error));
    }

    selectSport($event, sport: SportRequest) {
        this.selectedSport.newName = sport.name;
        this.selectedSport.oldName = sport.name;
        this.selectedSport.multipleTeamsAllowed = sport.multipleTeamsAllowed;
        this.selectedSport.sportOID = sport.sportOID;
        this.isFormActive = true;
      }

    closeForm($event) {
        this.isFormActive = false;
        this.updateGrid();
    }

    reportEvents($event, sport: SportRequest) {
        this.router.navigate(['/listEventByParam', { name: sport.name, item: '2' }]);
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
}