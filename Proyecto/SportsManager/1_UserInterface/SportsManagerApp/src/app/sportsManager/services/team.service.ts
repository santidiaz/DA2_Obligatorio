import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Observable } from "rxjs";
import { TeamRequest } from "../interfaces/team-request";
import { TeamModifyRequest } from "../interfaces/teammodifyrequest";
import { TeamRequestFilter } from "../interfaces/team-request-filter";

@Injectable()
export class TeamService {

  constructor(private baseService: BaseService) { }

  addTeam(request: TeamRequest): Observable<any> {
    let formData: FormData = new FormData();
    formData.append('Image', request.photo);
    formData.append('TeamName', request.name);
    formData.append('SportID', request.sportOID.toString());

    return this.baseService.post<FormData, any>('team', formData);
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

    return this.baseService.put<FormData, any>('team', formData);
  }

  getEventsByTeam(teamName: string): Observable<Array<Event>> {
    return this.baseService.get<Array<Event>>(`/team/${teamName}/events`, true);
  }
}