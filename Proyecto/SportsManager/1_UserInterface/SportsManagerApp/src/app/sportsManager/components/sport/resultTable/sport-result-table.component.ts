import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { SportService } from 'src/app/sportsManager/services/sport.service';
import { SportRequest } from 'src/app/sportsManager/interfaces/sportrequest';
import { TeamPoints } from 'src/app/sportsManager/interfaces/team-points';


@Component({
    selector: 'app-sport-result-table',
    templateUrl: './sport-result-table.component.html',
    styleUrls: ['./sport-result-table.component.css']
})
export class SportResultTable extends BaseComponent {

    sports: Array<SportRequest> = [];
    teamsPoints: Array<TeamPoints> = [];

    constructor(private sportService: SportService) {
        super();
    }

    componentOnInit() {
        this.sportService.getSports().subscribe(response => {
            this.sports = response;
        });
    }

    onChange(selectedValue: SportRequest) {
        //this.calendarEvents = [];
        //this.showEventDetails = false;
        //this.activeDayIsOpen = false;

        this.sportService.getSportResultTable(selectedValue.name).subscribe(response => {
            this.teamsPoints = response;
            //this.sportEvents = response;
            //this.loadCalendarEvents();
            //this.showCalendar = true;
        });
    }

}