import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { Component } from "@angular/core";
import { TeamRequest } from "src/app/sportsManager/interfaces/team-request";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { TeamService } from "src/app/sportsManager/services/team.service";
import { SportRequest } from "src/app/sportsManager/interfaces/sportrequest";
import { SportService } from "src/app/sportsManager/services/sport.service";

@Component({
  selector: 'app-addteam',
  templateUrl: './addteam.component.html',
  styleUrls: ['./addteam.component.css']
})

export class AddTeamComponent extends BaseComponent {

  formModel: TeamRequest;
  errorMessage: string = null;
  successMessage: string = null;
  sports: Array<SportRequest>;
  selectedDay: string = '';

  constructor(
    private sessionService: SessionService,
    private teamService: TeamService,
    private sportService: SportService) {
    super();
    this.formModel = { teamOID: 0, name: '', photo: null, sportOID: 0 };
  };

  ngOnInit() {
    this.successMessage = null;
    this.errorMessage = null;

    this.sportService.getSports().subscribe(response => {
      this.sports = response;
    });
  }

  upLoadSPor

  onSubmit() {

    this.teamService.addTeam(this.formModel)
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
}