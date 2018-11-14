import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { Component } from "@angular/core";
import { EventRequest } from "src/app/sportsManager/interfaces/event-request";
import { Event } from '../../../interfaces/event'
import { SessionService } from "src/app/sportsManager/services/session.service";
import { EventService } from "src/app/sportsManager/services/event.service";
import { EventRequestDynamic } from "src/app/sportsManager/interfaces/event-request-dynamic";
import { FixtureService } from "src/app/sportsManager/services/fixture.services";

@Component({
    selector: 'app-addevent',
    templateUrl: './addevent.component.html',
    styleUrls: ['./addevent.component.css']
})

export class AddEventComponent extends BaseComponent {

    isManualFormChecked: boolean;
    isDynamicFormChecked: boolean;
    eventsToAdd: Array<EventRequest> = [];
    auxEventFromDynamic: Array<Event> = [];
    auxEvent: EventRequest;

    constructor(
        private sessionService: SessionService,
        private eventService: EventService,
        private fixtureService: FixtureService) {
        super();;
    };

    ngOnInit() {
        //this.updateGrid();
        //this.successMessage = null;
        this.isManualFormChecked = true;
        this.isDynamicFormChecked = false;
    }

    changeView($event, item: Number) {
        if (item == 1) {
            this.isManualFormChecked = true;
            this.isDynamicFormChecked = false;
        }
        else {
            this.isManualFormChecked = false;
            this.isDynamicFormChecked = true;
        }
    }

    addEventTemp(EventRequest: EventRequest) {

        this.eventsToAdd.push(EventRequest);
    }

    addEventTempDynamic(eventsRequest: EventRequestDynamic) {

        // buscar los eventos a crear.
        this.fixtureService.getGenerateFixtures(eventsRequest).subscribe(response => {
            //this.auxEventFromDynamic = ;
            response.forEach(event => {
                this.auxEvent = { sportName: null, eventDate: null, teamNames: []};
                
                this.auxEvent.eventDate = event.initialDate;
                this.auxEvent.sportName = event.sportName;
                event.teams.forEach(tem => { this.auxEvent.teamNames.push(tem.name) });
    
                this.eventsToAdd.push(this.auxEvent);
            });
          });

        
    }

    deleteEvent($event, event: EventRequest) {
        const index: number = this.eventsToAdd.indexOf(event);
        if (index !== -1) {
            this.eventsToAdd.splice(index, 1);
        }
    }

    onSubmit() {

        this.eventsToAdd.forEach(element => {
            this.eventService.addEvent(element).subscribe(
                response => this.handleResponse(response),
                error => this.handleError(error));
        });
    }

    private handleResponse(response: any) {
        //this.sessionService.setSession(response);
        //this.errorMessage = null;
        //this.successMessage = 'Operation success';
    }

    private handleError(error: any) {
        //this.successMessage = null;
        //this.errorMessage = error.error;
    }

}