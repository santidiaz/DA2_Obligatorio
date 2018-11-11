import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { Component, Input, Output, EventEmitter } from "@angular/core";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { SportService } from "src/app/sportsManager/services/sport.service";
import { SportModifyRequest } from "src/app/sportsManager/interfaces/sportmodifyrequest";

@Component({
    selector: 'app-editsport',
    templateUrl: './editsport.component.html',
    styleUrls: ['./editsport.component.css']
  })

  export class EditSportComponent extends BaseComponent{

    formModel: SportModifyRequest;
    // @Input() sport: SportModifyRequest;
    _sport: SportModifyRequest;
    _oldSportName: string; // mapear a esta propiedad el nombre viejo.

    get sport() : SportModifyRequest{
      return this._sport; // una variable privada por ahi
    }

    @Input()
    set sport(u: SportModifyRequest){
        this._sport = u;
    }
    
    @Output() closeRequested = new EventEmitter<boolean>();

    errorMessage: string = null;
    successMessage: string = null;

    constructor(
        private sessionService: SessionService,
        private sportService: SportService) {
        super();
        this.formModel = { sportOID: 0, newName: '', oldName: '', allowdMultipleTeamsEvents: false};
      };

      ngOnInit() {
        this.successMessage = null;
        this.errorMessage = null;
        this.formModel.newName = this.sport.newName;
        this.formModel.oldName = this.sport.oldName;
        this.formModel.allowdMultipleTeamsEvents = this.sport.allowdMultipleTeamsEvents;
      }  

      onSubmit() {
        this.sportService.editSport(this.sport)
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