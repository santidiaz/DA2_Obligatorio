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

  editEvent(request: Event): Observable<any> {
    return this.baseService.put<Event, any>(`event/${request.id}`, request);
  }
}