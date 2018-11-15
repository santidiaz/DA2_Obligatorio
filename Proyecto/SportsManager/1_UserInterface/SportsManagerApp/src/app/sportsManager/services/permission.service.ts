import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { BaseService } from './base.service';
import { LoginRequest } from '../interfaces/login-request';
import { SessionUser } from '../interfaces/session-user';

@Injectable()
export class PermissionService {

  constructor(private baseService: BaseService) { }

  logIn(request: LoginRequest): Observable<SessionUser> {
    return this.baseService.post<LoginRequest, SessionUser>('authentication/login', request);
  }

  logOut(request: string): Observable<any> {
    return this.baseService.post<string, any>('authentication/logout', request);
  }
}