import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BaseService } from "./base.service";
import { Observable } from "rxjs";
import { TeamRequest } from "../interfaces/team-request";

@Injectable()
export class TeamService {

      constructor(private http: HttpClient, private baseService: BaseService) { }

      addTeam(request: TeamRequest): Observable<any> {
        let formData: FormData = new FormData(); 
        formData.append('Image', request.photo); 
        formData.append('TeamName', request.name); 
        formData.append('SportID', request.sportOID.toString()); 

        //return this.baseService.post<TeamRequest, any>('Team', request);
        return this.http.post(`http://localhost:5005/api/team`, formData)
      }

      getTeams(): Observable<TeamRequest[]> {
        return this.http.get<TeamRequest[]>(`http://localhost:5005/api/team`, { params: { orderAsc: 'false', teamName: '' }});
      }

      deleteTeam(teamName: string): Observable<any> {
        return this.http.delete(`http://localhost:5005/api/team/${teamName}`, {  observe: 'response' });
      }

      //editTeam(request: TeamModifyRequest): Observable<any> {
      //  return this.http.put(`http://localhost:5005/api/user/${request.oldName}`, {  observe: 'response' });
      //}
}