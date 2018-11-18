import { Component } from "@angular/core";
import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { SportRequest } from "src/app/sportsManager/interfaces/sportrequest";
import { SportService } from "src/app/sportsManager/services/sport.service";
import { SportModifyRequest } from "src/app/sportsManager/interfaces/sportmodifyrequest";


@Component({
    selector: 'app-listsports',
    templateUrl: './listsports.component.html',
    styleUrls: ['./listsports.component.css']
})


export class ListSportsComponent extends BaseComponent {

    sports: Array<SportRequest>;
    successMessage: string = null;
    isFormActive: boolean;
    selectedSport: SportModifyRequest;

    constructor(
        private sessionService: SessionService,
        private sportService: SportService) {
        super();
        this.selectedSport = { sportOID: 0, newName: '', oldName: '', multipleTeamsAllowed: false };
    };

    ngOnInit() {
        this.updateGrid();
        this.successMessage = null;
    }

    updateGrid(): void {
        this.sportService.getSports().subscribe(response => {
            this.sports = response;
        });
    }

    deleteSport($event, sport: SportRequest) {
        this.sportService.deleteSport(sport.name).subscribe(resp => {
            console.log(JSON.stringify(resp));
            this.successMessage = 'Operation success';
            this.updateGrid();
        });
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
}