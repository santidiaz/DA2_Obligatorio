import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Observable } from "rxjs";
import { SportRequest } from "../interfaces/sportrequest";
import { SportModifyRequest } from "../interfaces/sportmodifyrequest";
import { Event } from "../interfaces/event";
import { TeamPoints } from "../interfaces/team-points";

@Injectable()
export class SportService {

      constructor(private baseService: BaseService) { }

      addSport(request: SportRequest): Observable<any> {
        return this.baseService.post<SportRequest, any>('sport', request);
      }

      getSports(): Observable<Array<SportRequest>> {
        return this.baseService.get<Array<SportRequest>>('sport');
      }

      getSportEvents(sportName: string): Observable<Array<Event>> {
        return this.baseService.get<Array<Event>>(`sport/${sportName}/events`);
      }
      
      deleteSport(sportName: string): Observable<any> {
        return this.baseService.delete(`sport/${sportName}`, true);
      }

      getSportByName(sportName: string): Observable<SportRequest> {
        return this.baseService.get<SportRequest>(`sport/${sportName}`, true);
      }

      editSport(request: SportModifyRequest): Observable<any> {
        return this.baseService.put<SportModifyRequest, any>(`sport`, request);
      }

      getEventsBySport(sportName: string): Observable<Array<Event>> {
        return this.baseService.get<Array<Event>>(`sport/${sportName}/events`);
      }

      getSportResultTable(sportName: string): Observable<Array<TeamPoints>> {
        return this.baseService.get<Array<TeamPoints>>(`sport/${sportName}/resultTable`);
      }
}