import { Component, Input, Output, EventEmitter } from "@angular/core";
import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { TeamService } from "src/app/sportsManager/services/team.service";
import { SportService } from "src/app/sportsManager/services/sport.service";
import { SportRequest } from "src/app/sportsManager/interfaces/sportrequest";
import { TeamModifyRequest } from "src/app/sportsManager/interfaces/teammodifyrequest";
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-editteam',
  templateUrl: './editteam.component.html',
  styleUrls: ['./editteam.component.css']
})

export class EditTeamComponent extends BaseComponent {

  _team: TeamModifyRequest;

  @Input()
  set team(s: TeamModifyRequest) {
    this._team.newName = s.newName;
    this._team.oldName = s.oldName;
    this._team.photo = s.photo;
    this._team.photoString = s.photoString;
    //this._team.sportOID = s.sportOID;
  }
  get team(): TeamModifyRequest {
    return this._team; // una variable privada por ahi
  }

  @Output() closeRequested = new EventEmitter<boolean>();

  errorMessage: string = null;
  successMessage: string = null;
  //sports: Array<SportRequest>;

  constructor(
    private sessionService: SessionService,
    private teamService: TeamService,
    private sportService: SportService) {
    super();
    this._team = { teamOID: 0, newName: '', oldName: '', photo: null, photoString: '', sportOID: 0, newPhoto: null };
  };

  ngOnInit() {
    this.successMessage = null;
    this.errorMessage = null;

    //this.sportService.getSports().subscribe(response => {
    //  this.sports = response;
    //});
  }

  onSubmit() {

    if (this.team.newName == '' ||
      this.team.newPhoto == null ||
      this.team.oldName == '') {
      this.errorMessage = 'Complete all the required information';
    }
    else {
      this.errorMessage = null;
      this.team.photo = this.team.newPhoto;

      this.teamService.editTeam(this.team)
        .subscribe(
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

  file: File;
  onChange(event: EventTarget) {
    let eventObj: MSInputMethodContext = <MSInputMethodContext>event;
    let target: HTMLInputElement = <HTMLInputElement>eventObj.target;
    let files: FileList = target.files;
    this.file = files[0];
    this.team.newPhoto = this.file;
  }
}