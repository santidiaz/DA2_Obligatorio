import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { BaseService } from './base.service';

@Injectable()
export class EventService {

  constructor(private baseService: BaseService) { }

  getAllEvents(): Observable<Array<Event>> {
    return this.baseService.get<Array<Event>>('event', true);
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