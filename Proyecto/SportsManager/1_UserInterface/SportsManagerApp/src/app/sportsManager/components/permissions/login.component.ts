import { Component, SimpleChanges } from '@angular/core';
import { BaseComponent } from '../../shared/base.component';
import { PermissionService } from '../../services/permission.service';
import { LoginRequest } from '../../interfaces/login-request';
import { SessionService } from '../../services/session.service';
import { Router } from '@angular/router';
import { SessionUser } from '../../interfaces/session-user';
import { FormBuilder, FormGroup, Validators, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent {

  loginForm: LoginRequest;

  errorMessage: string;
  activateSubmit: boolean = true;

  constructor(
    private sessionService: SessionService,
    private permissionService: PermissionService,
    private router: Router) {
    super();
  }

  componentOnInit() {
    this.loginForm = { username: '', password: '' };
    this.errorMessage = undefined;
  }

  onSubmit() {
      this.activateSubmit = false;
      this.errorMessage = undefined;
      this.permissionService.logIn(this.loginForm).subscribe(
        response => this.handleResponse(response),
        error => this.handleError(error));
  }

  componentOnChanges(changes: SimpleChanges) {
    this.activateSubmit = true;
  }

  get formIsValid(): boolean {
    return this.loginForm.username !== undefined && 
    this.loginForm.password !== undefined && 
    this.loginForm.username !== '' &&
    this.loginForm.password !== '';
  }

  private handleResponse(response: SessionUser) {
    this.sessionService.setSession(response);

    if (this.sessionService.isAuthenticated) {
      this.router.navigate(['/events']);
    }
  }

  private handleError(response: any) {
    this.errorMessage = response.error;
    ;
  }
}