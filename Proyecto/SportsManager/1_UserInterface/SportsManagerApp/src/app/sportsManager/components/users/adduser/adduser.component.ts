// import { User } from '../../interfaces/user';
import { Component } from '@angular/core';
// import { BaseComponent } from '../../shared/base.component';
import { Router } from '@angular/router';
// import { UserService } from 'src/app/sportsManager/services/userService';
import { BaseComponent } from 'src/app/sportsManager/shared/base.component';
import { UserRequest } from 'src/app/sportsManager/interfaces/userrequest';
import { SessionService } from 'src/app/sportsManager/services/session.service';
import { UserService } from 'src/app/sportsManager/services/user.service';

@Component({
  selector: 'app-adduser',
  templateUrl: './adduser.component.html',
  styleUrls: ['./adduser.component.css']
})
export class AddUserComponent extends BaseComponent {

  formModel: UserRequest;
  errorMessage: string;

  constructor(
    private sessionService: SessionService,
    private userService: UserService) {
    super();
    this.formModel = { userOID: 0, userName: '', name: '', lastName: '', isAdmin: false, email: '', token: '' };
  };

  onSubmit() {
    this.userService.addUser(this.formModel)
      .subscribe(
        response => this.handleResponse(response),
        error => this.handleError(error));
  }

  private handleResponse(response: any) {
    this.sessionService.setSession(response);
  }

  private handleError(error: any) {
    this.errorMessage = error.error;
  }
}