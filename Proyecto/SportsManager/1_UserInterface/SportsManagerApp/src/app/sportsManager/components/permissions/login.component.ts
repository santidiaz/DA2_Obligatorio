import { Component } from '@angular/core';
import { BaseComponent } from '../../shared/base.component';
import { PermissionService } from '../../services/permission.service';
import { Login } from '../../interfaces/login';
import { SessionService } from '../../services/session.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent {

  formModel: Login;
  errorMessage: string;

  constructor(private _session: SessionService, private _permissionService : PermissionService) {
    super();
    this.formModel = { username: '', password: '' };
   }
 


  onSubmit(){
    let userName = `${this.formModel.username}`;
    let password = `${this.formModel.password}`;
    this._permissionService.logIn(userName, password)
                .subscribe(response => this.handleResponse(response),
                        error => this.handleError(error));
  }

  private handleResponse(response: any){
    let res: any = response;
    this._session.setToken(res.token);
  }

  private handleError(error: any){
    this.errorMessage = error.error;
  }
}