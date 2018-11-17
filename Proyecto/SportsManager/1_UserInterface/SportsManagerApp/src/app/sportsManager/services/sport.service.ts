import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BaseService } from "./base.service";
import { Observable } from "rxjs";
import { SportRequest } from "../interfaces/sportrequest";
import { SportModifyRequest } from "../interfaces/sportmodifyrequest";
import { Event } from "../interfaces/event";

@Injectable()
export class SportService {

      constructor(private http: HttpClient, private baseService: BaseService) { }

      addSport(request: SportRequest): Observable<any> {
        return this.baseService.post<SportRequest, any>('sport', request);
      }

      getSports(): Observable<Array<SportRequest>> {
        return this.baseService.get<Array<SportRequest>>('sport', true);
      }

      getSportEvents(sportName: string): Observable<Array<Event>> {
        return this.baseService.get<Array<Event>>(`sport/${sportName}/events`, true);
      }
      
      deleteSport(sportName: string): Observable<any> {
        return this.http.delete(`http://localhost:5005/api/sport/${sportName}`, {  observe: 'response' });
      }

      editSport(request: SportModifyRequest): Observable<any> {
        return this.http.put(`http://localhost:5005/api/sport/${request.oldName}`, {  observe: 'response' });
      }
}