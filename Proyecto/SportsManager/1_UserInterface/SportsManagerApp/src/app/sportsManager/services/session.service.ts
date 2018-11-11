import { Injectable } from '@angular/core';
import { SessionUser } from '../interfaces/session-user';

const TOKEN = 'token';
const IS_ADMIN = 'isAdministrator';

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

    setSession(session: SessionUser) {
        localStorage.setItem(TOKEN, session.token);
        localStorage.setItem(IS_ADMIN, String(session.isAdmin));
    }

    isAdmin(): boolean {
        return localStorage.getItem(IS_ADMIN) === 'true';
    }

    logOff() {
        localStorage.removeItem(TOKEN);
        localStorage.removeItem(IS_ADMIN);
    }
}
