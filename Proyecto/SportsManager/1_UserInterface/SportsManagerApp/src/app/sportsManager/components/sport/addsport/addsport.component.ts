import { Component } from '@angular/core';
import { BaseComponent } from 'src/app/sportsManager/shared/base.component';
import { SportRequest } from 'src/app/sportsManager/interfaces/sportrequest';
import { SessionService } from 'src/app/sportsManager/services/session.service';
import { SportService } from 'src/app/sportsManager/services/sport.service';

@Component({
  selector: 'app-addsport',
  templateUrl: './addsport.component.html',
  styleUrls: ['./addsport.component.css']
})


export class AddSportComponent extends BaseComponent {

  formModel: SportRequest;
  errorMessage: string = null;
  successMessage: string = null;

  constructor(
    private sessionService: SessionService,
    private sportService: SportService) {
    super();
    this.clearModel();
  };

  clearModel(){
    this.formModel = { id: 0, name: '', multipleTeamsAllowed: false, teams: [] };
  }

  ngOnInit() {
    this.successMessage = null;
    this.errorMessage = null;
  }

  onSubmit() {

    if (this.formModel.name == '') {
      this.errorMessage = 'Complete all the required information';
    }
    else {
      this.errorMessage = null;
      this.sportService.addSport(this.formModel)
        .subscribe(
          response => this.handleResponse(response),
          error => this.handleError(error));
    }


  }

  private handleResponse(response: any) {
    //this.sessionService.setSession(response);
    this.errorMessage = null;
    this.successMessage = 'Operation success';
    this.clearModel();
  }

  private handleError(error: any) {
    this.successMessage = null;
    this.errorMessage = error.error;
  }
}