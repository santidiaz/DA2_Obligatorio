import { Component } from '@angular/core';
import { BaseComponent } from '../../shared/base.component';
import { SessionService } from '../../services/session.service';
import { Router } from '@angular/router';

@Component({
    selector: 'nav-bar',
    templateUrl: './nav-bar.html',
    styleUrls: ['./nav-bar.css']
})
export class NavigationBar extends BaseComponent {

    title = 'Sports Manager';

    constructor(private sessionService: SessionService, private router: Router) {
        super();
    }

    get isLoggedIn(): boolean {
        let algo = this.sessionService.isAuthenticated();
        return algo;
    }

    get isAdmin(): boolean {
        return this.sessionService.isAdmin();
    }

    logOut($event){
        this.sessionService.logOff();
        this.router.navigate(['/login']);
    }
}