import { Injectable } from '@angular/core';
import { SessionUser } from '../interfaces/session-user';

const TOKEN = 'token';
const IS_ADMIN = 'isAdministrator';
const USER_NAME = 'userName';

@Injectable()
export class SessionService {
    constructor() { }

    isAuthenticated(): boolean {
        return this.getToken() !== null;
    }

    setToken(newToken: string): void {
        localStorage.setItem(TOKEN, newToken);
    }

    removeToken(): void {
        localStorage.removeItem(TOKEN);
    }

    getToken(): string {
        return localStorage.getItem(TOKEN);
    }

    getCurrentUserName(): string {
        return localStorage.getItem(USER_NAME);
    }

    setSession(session: SessionUser) {
        localStorage.setItem(TOKEN, session.token);
        localStorage.setItem(USER_NAME, session.userName);
        localStorage.setItem(IS_ADMIN, String(session.isAdmin));
    }

    isAdmin(): boolean {
        return localStorage.getItem(IS_ADMIN) === 'true';
    }

    logOff() {
        localStorage.removeItem(USER_NAME);
        localStorage.removeItem(TOKEN);
        localStorage.removeItem(IS_ADMIN);
    }
}