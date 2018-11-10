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

    constructor(
        private sessionService: SessionService,
        private userService: UserService) {
        super();
      };

      ngOnInit() {
        this.updateGrid();
      }

      updateGrid(): void {
        this.userService.getUsers().subscribe(response => {
          this.users = response;
        });
      }
}