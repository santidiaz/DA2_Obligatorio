import { Component } from '@angular/core';
import { BaseComponent } from '../../shared/base.component';
import { PermissionService } from '../../services/permission.service';
import { LoginRequest } from '../../interfaces/login-request';
import { SessionService } from '../../services/session.service';
import { Router } from '@angular/router';
import { SessionUser } from '../../interfaces/session-user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent {

  formModel: LoginRequest;
  errorMessage: string;

  constructor(
    private sessionService: SessionService, 
    private permissionService : PermissionService,
    private router: Router) {
    super();
    this.formModel = { username: '', password: '' };
   }

  onSubmit(){
    this.permissionService.logIn(this.formModel)
      .subscribe(
        response => this.handleResponse(response), 
        error => this.handleError(error));
  }


  private handleResponse(response: SessionUser) {
    this.sessionService.setSession(response);



    /*if (this.sessionService.isAuthenticated) {
      this.router.navigate([this.sessionService.attemptedUrl]);
    }*/
  }

  private handleError(error: any) {
    this.errorMessage = error.error;
  }
}