import { Component, Input, Output, EventEmitter } from "@angular/core";
import { UserRequest } from "src/app/sportsManager/interfaces/userrequest";
import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { UserService } from "src/app/sportsManager/services/user.service";

@Component({
    selector: 'app-useredit',
    templateUrl: './useredit.component.html',
    styleUrls: ['./useredit.component.css']
  })

  export class UserEditComponent extends BaseComponent{

    formModel: UserRequest;
    // @Input() user: UserRequest;
    _user: UserRequest;

    get user() : UserRequest{
      return this._user; // una variable privada por ahi
    }

    @Input()
    set user(u: UserRequest){
        this._user = u;
    }
    
    @Output() closeRequested = new EventEmitter<boolean>();

    errorMessage: string = null;
    successMessage: string = null;

    constructor(
      private sessionService: SessionService,
      private userService: UserService) {
      super();
      this.formModel = { userOID: 0, userName: '', name: '', lastName: '', isAdmin: false, email: '', password: ''};
    };

    ngOnInit() {
      this.successMessage = null;
      this.errorMessage = null;
      this.formModel.userOID = this.user.userOID, 
      this.formModel.userName = this.user.userName, 
      this.formModel.name = this.user.name, 
      this.formModel.lastName = this.user.lastName, 
      this.formModel.isAdmin = this.user.isAdmin, 
      this.formModel.email = this.user.email
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
    }
  
    private handleError(error: any) {
      this.successMessage = null;
      this.errorMessage = error.error;
    }
    
    cancel() {
      this.closeRequested.emit(true);
    }
}