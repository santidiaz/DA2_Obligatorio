import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { LoginRequest } from '../interfaces/login-request';

import { Observable } from 'rxjs';
import { SessionUser } from '../interfaces/session-user';
/*
const httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Origin': '*',
      'Content-Type': 'application/json'
    })
};*/

@Injectable()
export class PermissionService {

  constructor(private baseService: BaseService) { }

  logIn(request: LoginRequest): Observable<SessionUser> {
    return this.baseService.post<LoginRequest, SessionUser>('authentication/login', request);
  }

  /*registerUser(request: UserRequest): Observable<any> {
    return this.baseApiService.post<LoginRequest, Session>('users', request, true);
  }*/



    /*constructor(protected _http: HttpClient, protected _session:SessionService) {
      super(_http);
    }
    
    logIn(inputUserName: string, inputPassword: string) {      
      return this._http.post( 
        `${super.getAddress()}/api/authentication/login`,
        JSON.stringify({ userName: inputUserName, password: inputPassword }),
        httpOptions).pipe(
                    map((response: Response) => <any>response,
                    catchError(this.handleError)));
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }*/
}