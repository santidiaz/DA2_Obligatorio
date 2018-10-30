import { Injectable } from '@angular/core';

@Injectable()
export class SessionService {

    private _currentToken: string;
    private _currentUserName: string;
    private _isAdmin: boolean;

    constructor() { }

    isAuthenticated(): boolean {
        return this._currentToken !== null;
    }

    setToken(token: string): void {
        this._currentToken = token;
    }

    removeToken(): void {
        this._currentToken = undefined;
    }

    getToken(): string {
        return this._currentToken;
    }

    isAdmin(): boolean {
        return this._isAdmin;
    }

    setAdminFlag(value: boolean) {
        this._isAdmin = value;
    }

    setUserName(value: string) {
        this._currentUserName = value;
    }

    getUserName():string {
        return this._currentUserName;
    }
}
