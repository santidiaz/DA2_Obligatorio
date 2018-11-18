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

    post<T, Y>(url: string, request: T, isTokenRequired: boolean = true): Observable<Y> {
        return this.http.post<any>(`${environment.apiUrl}${url}`, request, { headers: this.getHeader(isTokenRequired) });
    }

    get<Y>(url: string, isTokenRequired: boolean = true, params: any = undefined): Observable<Y> {
        return this.http.get<any>(`${environment.apiUrl}${url}`, { headers: this.getHeader(isTokenRequired), params: this.generateParams(params) });
    }

    put<T, Y>(url: string, request: T, isTokenRequired: boolean = true): Observable<Y> {
        return this.http.put<any>(`${environment.apiUrl}${url}`, request, { headers: this.getHeader(isTokenRequired) });
    }

    delete<Y>(url: string, isTokenRequired: boolean = true): Observable<Y> {
        return this.http.delete<any>(`${environment.apiUrl}${url}`, { headers: this.getHeader(isTokenRequired) });
    }

    private getHeader(tokenRequired: boolean): HttpHeaders {
        return !tokenRequired ? this.basicHeaderConfig : this.tokenHeaderConfig;
    }

    private generateParams<T>(data: T): HttpParams {
        if(data != undefined){
            let httpParams = new HttpParams();
            Object.keys(data).forEach(function (key) {
                 httpParams = httpParams.append(key, data[key]);
            });
            return httpParams;
        }
        return undefined;
    }

    public getToken() : any{
        this.sessionService.getToken();
    }
}
