import { Component } from '@angular/core';
import { BaseComponent } from '../../shared/base.component';
import { SessionService } from '../../services/session.service';
import { Router } from '@angular/router';
import { PermissionService } from '../../services/permission.service';

@Component({
    selector: 'nav-bar',
    templateUrl: './nav-bar.html',
    styleUrls: ['./nav-bar.css']
})
export class NavigationBar extends BaseComponent {

    title = 'Sports Manager';

    constructor(private permissionService: PermissionService, private sessionService: SessionService, private router: Router) {
        super();
    }

    get isLoggedIn(): boolean {
        return this.sessionService.isAuthenticated();
    }

    get isAdmin(): boolean {
        return this.sessionService.isAdmin();
    }

    logOut($event){
        this.permissionService.logOut(this.sessionService.getCurrentUserName());
        this.sessionService.logOff();
        this.router.navigate(['/login']);
    }
}