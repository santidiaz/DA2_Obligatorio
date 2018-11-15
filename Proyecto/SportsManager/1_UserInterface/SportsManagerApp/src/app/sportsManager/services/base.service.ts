import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { SessionService } from '../services/session.service';
import { environment } from '../../../environments/environment';

@Injectable()
export class BaseService {

    private basicHeaderConfig = new HttpHeaders({ 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' });
    private tokenHeaderConfig = new HttpHeaders(
        { 
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Authorization': this.sessionService.getToken()
        });

    constructor(private http: HttpClient, private sessionService: SessionService) { }    

    post<T, Y>(url: string, request: T, isTokenRequired: boolean = false): Observable<Y> {
        return this.http.post<any>(`${environment.apiUrl}${url}`, request, { headers: this.getHeader(isTokenRequired) });
    }

    get<Y>(url: string, isTokenRequired: boolean = false, params: HttpParams = undefined): Observable<Y> {
        return this.http.get<any>(`${environment.apiUrl}${url}`, { headers: this.getHeader(isTokenRequired) }, { params });
    }

    put<T, Y>(url: string, request: T, isTokenRequired: boolean = false): Observable<Y> {
        return this.http.put<any>(`${environment.apiUrl}${url}`, request, { headers: this.getHeader(isTokenRequired) });
    }

    delete<Y>(url: string, isTokenRequired: boolean = false): Observable<Y> {
        return this.http.delete<any>(`${environment.apiUrl}${url}`, { headers: this.getHeader(isTokenRequired) });
    }

    private getHeader(tokenRequired: boolean): HttpHeaders {
        return !tokenRequired ? this.basicHeaderConfig : this.tokenHeaderConfig;
    }
}
