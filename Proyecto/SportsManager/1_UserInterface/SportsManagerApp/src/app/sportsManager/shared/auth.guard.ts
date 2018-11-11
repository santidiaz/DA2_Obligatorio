import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivateChild, NavigationExtras } from '@angular/router';
import { Observable } from 'rxjs';
import { SessionService } from '../services/session.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private sessionService: SessionService, private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    return this.hasPermission(next.data.onlyAdmin, state.url);
  }

  hasPermission(onlyAdmin: boolean, attemptedUrl: string) {
    let result: boolean = true;
    if (!this.sessionService.isAuthenticated()) {
      this.router.navigate(['/login']);
      result = false;
    }


    if (result && onlyAdmin) {
      const isValid = this.sessionService.isAdmin();
      if (!isValid) {
        const navigationExtras: NavigationExtras = {
          queryParams: { 'message': `${attemptedUrl} requires administrator rights` }
        };

        this.router.navigate(['/event'], navigationExtras);
        result = false;
      }
    }

    return result;
  }
}
