import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class BaseService {

    private _address: string;

    constructor(public http: HttpClient){
        this.getAppSettings().subscribe(
            (data: any) => this._address = data.global.address);
    }

    private getAppSettings(): Observable<any> {
        return this.http.get("../../../assets/appsettings.json");
    }

    public getAddress() : string{
        return this._address;
    }
}