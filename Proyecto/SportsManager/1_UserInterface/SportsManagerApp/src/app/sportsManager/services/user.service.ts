import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BaseService } from "./base.service";
import { UserRequest } from "../interfaces/userrequest";
import { Observable } from "rxjs";
import { UserTeam } from "../interfaces/user-team";
import { AddFavoriteRequest } from "../interfaces/addfavoriterequest";

@Injectable()
export class UserService {

  constructor(private http: HttpClient, private baseService: BaseService) { }

  addUser(request: UserRequest): Observable<any> {
    return this.baseService.post<UserRequest, any>('user', request);
  }

  getUsers(): Observable<UserRequest[]> {
    return this.http.get<UserRequest[]>(`http://localhost:5005/api/user`);
  }

  deleteUser(userName: string): Observable<any> {
    return this.http.delete(`http://localhost:5005/api/user/${userName}`, { observe: 'response' });
  }

  editUser(request: UserRequest): Observable<any> {
    return this.baseService.put<UserRequest, any>(`user/${request.userName}`, request);
  }

  getUserFavoriteTeams(userName: string): Observable<UserTeam[]> {
    return this.http.get<UserTeam[]>(`http://localhost:5005/api/user/${userName}/favorites/`);
  }

  addUserFavoriteTeams(userName: string, request: AddFavoriteRequest): Observable<any> {
    return this.baseService.post<AddFavoriteRequest, any>(`/user/${userName}/addfavorites/`, request);
  }

  deleteUserFavoriteTeam(userName: string, teamid: number): Observable<any> {
    return this.http.delete(`http://localhost:5005/api/user/${userName}/favorite/${teamid}`, { observe: 'response' });
  }

}