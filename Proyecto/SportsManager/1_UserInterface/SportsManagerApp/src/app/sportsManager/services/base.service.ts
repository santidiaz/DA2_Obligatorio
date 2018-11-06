import { HttpClient, HttpHeaders } from '@angular/common/http';
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

    private getHeader(tokenRequired: boolean): HttpHeaders {
        return !tokenRequired ? this.basicHeaderConfig : this.tokenHeaderConfig;
    }





   /* private _address: string;

    constructor(public http: HttpClient) {
        this.getAppSettings().subscribe(
            (data: any) => this._address = data.global.address);
    }

    private getAppSettings(): Observable<any> {
        return this.http.get('../../../assets/appsettings.json');
    }

    public getAddress(): string {
        return this._address;
    }*/
}
