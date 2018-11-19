import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { BaseService } from './base.service';
import { LoginRequest } from '../interfaces/login-request';
import { SessionUser } from '../interfaces/session-user';

@Injectable()
export class PermissionService {

  constructor(private baseService: BaseService) { }

  logIn(request: LoginRequest): Observable<SessionUser> {
    return this.baseService.post<LoginRequest, SessionUser>('authentication/login', request, false);
  }

  logOut(inputUserName: string): Observable<any> {
    let request = { userName: inputUserName };
    return this.baseService.post<any, any>('authentication/logout', request, false);
  }
}