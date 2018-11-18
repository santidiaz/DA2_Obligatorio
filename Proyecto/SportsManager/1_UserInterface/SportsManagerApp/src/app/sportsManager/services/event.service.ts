import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { BaseService } from './base.service';
import { EventRequest } from '../interfaces/event-request';
import { Event } from '../interfaces/event';

@Injectable()
export class EventService {

  constructor(private http: HttpClient, private baseService: BaseService) { }

  getAllEvents(): Observable<Array<Event>> {
    return this.baseService.get<Array<Event>>('event', true);
  }

  getEventById(eventId: number): Observable<Event> {
    return this.baseService.get<Event>(`event/${eventId}`, true);
  }


  addEvent(request: EventRequest): Observable<any> {
    return this.baseService.post<EventRequest, any>('event', request, true);
  }

  deleteEvent(eventId: number): Observable<any> {
    return this.baseService.delete(`event/${eventId}`, true);
  }

  /*addEvent(request: EventRequest): Observable<any> {
    return this.http.post(`http://localhost:5005/api/event`, {  observe: 'response' });
  }/*

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