import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BaseService } from "./base.service";
import { Observable } from "rxjs";
import { TeamRequest } from "../interfaces/team-request";
import { TeamModifyRequest } from "../interfaces/teammodifyrequest";
import { TeamRequestFilter } from "../interfaces/team-request-filter";

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

      getTeams(teamRequestFilter: TeamRequestFilter): Observable<TeamRequest[]> {
        var order = teamRequestFilter.orderAsc ? 'true' : 'false';
        let algo = { orderAsc: order, teamName: teamRequestFilter.teamName };
        return this.baseService.get<TeamRequest[]>('team', true, algo);

        /*var auxHeaders = new HttpHeaders(
          { 
              'Content-Type': 'application/json',
              'Access-Control-Allow-Origin': '*',
              'Authorization': this.baseService.getToken() 
          });

        
        return this.baseService.get<TeamRequest[]>(`team`, 
                                            { headers: auxHeaders, 
                                              params: { orderAsc: order, teamName: teamRequestFilter.teamName }
                                            });*/
      }

      deleteTeam(teamName: string): Observable<any> {
        return this.baseService.delete(`team/${teamName}`);
      }

      editTeam(request: TeamModifyRequest): Observable<any> {
        let formData: FormData = new FormData(); 
        formData.append('Image', request.photo); 
        formData.append('OldName', request.oldName); 
        formData.append('NewName', request.newName); 

        return this.http.put(`http://localhost:5005/api/team`, formData)
      }

      getEventsByTeam(teamName: string): Observable<Array<Event>> {
        return this.baseService.get<Array<Event>>(`/team/${teamName}/events`, true);
      }
}