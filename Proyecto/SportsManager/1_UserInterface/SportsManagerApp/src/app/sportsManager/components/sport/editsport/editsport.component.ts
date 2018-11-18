import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { Component, Input, Output, EventEmitter } from "@angular/core";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { SportService } from "src/app/sportsManager/services/sport.service";
import { SportModifyRequest } from "src/app/sportsManager/interfaces/sportmodifyrequest";
import { SportRequest } from "src/app/sportsManager/interfaces/sportrequest";

@Component({
  selector: 'app-editsport',
  templateUrl: './editsport.component.html',
  styleUrls: ['./editsport.component.css']
})

export class EditSportComponent extends BaseComponent {

  _sport: SportModifyRequest;

  @Input()
  set sport(s: SportModifyRequest) {
    this._sport.newName = s.newName;
    this._sport.oldName = s.oldName;
    this._sport.multipleTeamsAllowed = s.multipleTeamsAllowed;
    this._sport.sportOID = s.sportOID;
  }
  get sport(): SportModifyRequest {
    return this._sport; // una variable privada por ahi
  }

  @Output() closeRequested = new EventEmitter<boolean>();

  errorMessage: string = null;
  successMessage: string = null;

  constructor(
    private sessionService: SessionService,
    private sportService: SportService) {
    super();
    this._sport = { sportOID: 0, newName: '', oldName: '', multipleTeamsAllowed: false };
  };

  ngOnInit() {
    this.successMessage = null;
    this.errorMessage = null;
  }

  onSubmit() {

    if (this.sport.newName == '' ||
      this.sport.oldName == '') {
      this.errorMessage = 'Complete all the required information';
    }
    else {
      this.errorMessage = null;
      this.sportService.editSport(this.sport)
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
}