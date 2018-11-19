import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { BaseService } from './base.service';
import { EventRequest } from '../interfaces/event-request';
import { Event } from '../interfaces/event';
import { SetupEventResultRequest } from '../interfaces/setup-event-result-request';

@Injectable()
export class EventService {

  constructor(private http: HttpClient, private baseService: BaseService) { }

  getAllEvents(): Observable<Array<Event>> {
    return this.baseService.get<Array<Event>>('event');
  }

  getEventById(eventId: number): Observable<Event> {
    return this.baseService.get<Event>(`event/${eventId}`);
  }

  addEvent(request: EventRequest): Observable<any> {
    return this.baseService.post<EventRequest, any>('event', request);
  }

  deleteEvent(eventId: number): Observable<any> {
    return this.baseService.delete(`event/${eventId}`);
  }

  setupEventResult(eventId: number, teamsResult: SetupEventResultRequest): Observable<any> {
    return this.baseService.put<SetupEventResultRequest, any>(`event/SetupResult/${eventId}`, teamsResult)
  }
  editEvent(request: Event): Observable<any> {
    return this.baseService.put<Event, any>(`event/${request.id}`, request);
  }
}