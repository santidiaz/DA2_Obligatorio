import { Component } from '@angular/core';
import { BaseComponent } from '../../shared/base.component';
import { SessionService } from '../../services/session.service';

@Component({
    selector: 'nav-bar',
    templateUrl: './nav-bar.html',
    styleUrls: ['./nav-bar.css']
})
export class NavigationBar extends BaseComponent {

    protected _hasPermission: boolean = false;

    constructor(private sessionService: SessionService) {
        super();
    }

    componentOnInit() {
        this._hasPermission = this.sessionService.isAdmin();
    }
}