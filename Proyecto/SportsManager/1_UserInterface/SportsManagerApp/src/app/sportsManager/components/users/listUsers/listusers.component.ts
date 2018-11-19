import { Component } from "@angular/core";
import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { UserService } from "src/app/sportsManager/services/user.service";
import { UserRequest } from "src/app/sportsManager/interfaces/userrequest";


@Component({
    selector: 'app-listUsers',
    templateUrl: './listUsers.component.html',
    styleUrls: ['./listUsers.component.css']
})


export class ListUsersComponent extends BaseComponent {

    users: Array<UserRequest>;
    successMessage: string = null;
    isFormActive: boolean;
    selectedUser: UserRequest;
    isAdmin: boolean;

    constructor(
        private sessionService: SessionService,
        private userService: UserService) {
        super();
    };

    componentOnInit() {
        this.isAdmin = this.sessionService.isAdmin();
        this.updateGrid();
        this.successMessage = null;
    }

    updateGrid(): void {
        this.userService.getUsers().subscribe(response => {
            this.users = response;
        });
    }

    deleteUser($event, user: UserRequest) {
        this.userService.deleteUser(user.userName).subscribe(resp => {
            console.log(JSON.stringify(resp));
            this.successMessage = 'Operation success';
            this.updateGrid();
        });
    }

    selectUser(event: any, user: UserRequest) {
        this.selectedUser = user;
        this.isFormActive = true;
      }

    closeForm($event) {
        this.isFormActive = false;
        this.updateGrid();
    }
}