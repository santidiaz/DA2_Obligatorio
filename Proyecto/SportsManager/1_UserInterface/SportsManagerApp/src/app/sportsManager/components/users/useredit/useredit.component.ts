import { Component, Input, Output, EventEmitter } from "@angular/core";
import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { UserService } from "src/app/sportsManager/services/user.service";
import { UserRequest } from "src/app/sportsManager/interfaces/userrequest";

@Component({
    selector: 'app-useredit',
    templateUrl: './useredit.component.html',
    styleUrls: ['./useredit.component.css']
  })

  export class UserEditComponent extends BaseComponent{

    // @Input() user: UserRequest;
    //_user: UserRequest;
    _user: UserRequest;
 
    @Input()
    set user(u: UserRequest){
        this._user.userName = u.userName;
        this._user.userOID = u.userOID;
        this._user.password = '';
        this._user.name = u.name;
        this._user.lastName = u.lastName;
        this._user.email = u.email;
        this._user.isAdmin = u.isAdmin;
    }
    get user() : UserRequest{
      return this._user; // una variable privada por ahi
    }

    @Output() closeRequested = new EventEmitter<boolean>();

    errorMessage: string = null;
    successMessage: string = null;

    constructor(
      private sessionService: SessionService,
      private userService: UserService) {
      super();
      this._user = { userOID: 0, userName: '', name: '', lastName: '', isAdmin: false, email: '', password: ''};
    };

    ngOnInit() {
      this.successMessage = null;
      this.errorMessage = null;
    }  

    onSubmit() {
      this.userService.editUser(this.user)
        .subscribe(
          response => this.handleResponse(response),
          error => this.handleError(error));
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