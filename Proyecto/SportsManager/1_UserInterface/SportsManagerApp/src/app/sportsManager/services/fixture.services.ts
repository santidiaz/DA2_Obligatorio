import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BaseService } from "./base.service";
import { Observable } from "rxjs";
import { Event } from '../interfaces/event'
import { EventRequestDynamic } from "../interfaces/event-request-dynamic";

@Injectable()
export class FixtureService {

      constructor(private http: HttpClient, private baseService: BaseService) { }

      getFixtures(): Observable<string[]> {
        return this.baseService.get<string[]>(`fixture`, true);
      }

      getGenerateFixtures(fromModel: EventRequestDynamic): Observable<Event[]> {
        return this.baseService.post<EventRequestDynamic, Event[]>('fixture', fromModel);
      }

    }