import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Http, Response } from '@angular/http';
import { BaseService } from './baseService';

import { Observable } from 'rxjs';
import { map, catchError} from 'rxjs/operators';
import { SessionService } from './session.service';

const httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Origin': '*',
      'Content-Type': 'application/json'
    })
  };

@Injectable()
export class PermissionService extends BaseService {

    constructor(protected _http: HttpClient, protected _session:SessionService) {
      super(_http);
    }
    
    logIn(inputUserName: string, inputPassword: string) {      
      return this._http.post( 
        `${super.getAddress()}/api/authentication/login`,
        JSON.stringify({ userName: inputUserName, password: inputPassword }),
        httpOptions).pipe(
                    map((response: Response) => <any>response.json(),
                    catchError(this.handleError)));
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}