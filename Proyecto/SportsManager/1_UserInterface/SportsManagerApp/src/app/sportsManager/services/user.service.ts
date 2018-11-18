import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { UserRequest } from "../interfaces/userrequest";
import { Observable } from "rxjs";
import { UserTeam } from "../interfaces/user-team";
import { Event } from "../interfaces/event";
import { AddFavoriteRequest } from "../interfaces/addfavoriterequest";

@Injectable()
export class UserService {

  constructor(private baseService: BaseService) { }

  addUser(request: UserRequest): Observable<any> {
    return this.baseService.post<UserRequest, any>('user', request);
  }

  getUsers(): Observable<UserRequest[]> {
    return this.baseService.get<UserRequest[]>('user');
  }

  deleteUser(userName: string): Observable<any> {
    return this.baseService.delete<any>(`user/${userName}`);
  }

  editUser(request: UserRequest): Observable<any> {
    return this.baseService.put<UserRequest, any>(`user/${request.userName}`, request);
  }

  getUserFavoriteTeams(userName: string): Observable<UserTeam[]> {
    return this.baseService.get<UserTeam[]>(`user/${userName}/favorites/`);
  }

  addUserFavoriteTeams(userName: string, request: AddFavoriteRequest): Observable<any> {
    return this.baseService.post<AddFavoriteRequest, any>(`/user/${userName}/addfavorites/`, request);
  }

  deleteUserFavoriteTeam(userName: string, teamid: number): Observable<any> {
    return this.baseService.delete(`user/${userName}/favorite/${teamid}`);
  }

  getUserFavouriteTeamsEvents(userName: string): Observable<Array<Event>> {
    return this.baseService.get<Array<Event>>(`user/${userName}/favoriteTeamComments/`);
  }
}