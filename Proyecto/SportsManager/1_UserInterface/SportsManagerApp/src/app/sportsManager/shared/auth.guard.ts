import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivateChild, NavigationExtras } from '@angular/router';
import { Observable } from 'rxjs';
import { SessionService } from '../services/session.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild {
  
  constructor(private sessionService: SessionService, private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    return this.isAdmin(next.data.isAdmin, state.url);
  }

  isAdmin(isAdmin: boolean, attemptedUrl: string) {
      let result: boolean = true;
      
      if(isAdmin){
        const isValid = this.sessionService.isAdmin();
        if (!isValid) {
          const navigationExtras: NavigationExtras = {
            queryParams: { 'message': `${attemptedUrl} requires administrator rights` }
          };
  
          this.router.navigate(['**'], navigationExtras);
        }
      }

      return result;
    }
}
