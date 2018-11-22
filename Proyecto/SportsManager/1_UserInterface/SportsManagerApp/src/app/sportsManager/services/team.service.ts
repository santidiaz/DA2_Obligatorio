import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Observable } from "rxjs";
import { TeamRequest } from "../interfaces/team-request";
import { TeamModifyRequest } from "../interfaces/teammodifyrequest";
import { TeamRequestFilter } from "../interfaces/team-request-filter";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { SessionService } from "./session.service";
import { environment } from '../../../environments/environment';

@Injectable()
export class TeamService {

  constructor(private http: HttpClient, private sessionService: SessionService, private baseService: BaseService) { }

  addTeam(request: TeamRequest): Observable<any> {
    let formData: FormData = new FormData();
    formData.append('Image', request.photo);
    formData.append('TeamName', request.name);
    formData.append('SportID', request.sportOID.toString());

    var url = 'team';
    return this.http.post(`${environment.apiUrl}${url}`, formData);
  }

  getTeams(teamRequestFilter: TeamRequestFilter): Observable<TeamRequest[]> {
    var order = teamRequestFilter.orderAsc ? 'true' : 'false';
    let params = { orderAsc: order, teamName: teamRequestFilter.teamName };
    return this.baseService.get<TeamRequest[]>('team', true, params);
  }

  deleteTeam(teamName: string): Observable<any> {
    return this.baseService.delete(`team/${teamName}`);
  }

  editTeam(request: TeamModifyRequest): Observable<any> {
    let formData: FormData = new FormData();
    formData.append('Image', request.photo);
    formData.append('OldName', request.oldName);
    formData.append('NewName', request.newName);

    var url = 'team';
    return this.http.put(`${environment.apiUrl}${url}`, formData);
  }

  getEventsByTeam(teamName: string): Observable<Array<Event>> {
    return this.baseService.get<Array<Event>>(`/team/${teamName}/events`, true);
  }
}