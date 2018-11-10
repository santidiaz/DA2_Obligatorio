import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BaseService } from "./base.service";
import { UserRequest } from "../interfaces/userrequest";
import { Observable } from "rxjs";

@Injectable()
export class UserService {

      constructor(private http: HttpClient, private baseService: BaseService) { }

      addUser(request: UserRequest): Observable<any> {
        return this.baseService.post<UserRequest, any>('user', request);
      }

      getUsers(): Observable<UserRequest[]> {
        return this.http.get<UserRequest[]>(`http://localhost:5005/api/user`);
      }
}