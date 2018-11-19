import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { Component } from "@angular/core";
import { EventRequest } from "src/app/sportsManager/interfaces/event-request";
import { Event } from '../../../interfaces/event'
import { SessionService } from "src/app/sportsManager/services/session.service";
import { EventService } from "src/app/sportsManager/services/event.service";
import { EventRequestDynamic } from "src/app/sportsManager/interfaces/event-request-dynamic";
import { FixtureService } from "src/app/sportsManager/services/fixture.services";
import { DomSanitizer } from '@angular/platform-browser';

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
    errorMessage: string = null;
    successMessage: string = null;

    constructor(
        private sessionService: SessionService,
        private eventService: EventService,
        private fixtureService: FixtureService,
        sanitizer: DomSanitizer) {
        super();;
    };

    ngOnInit() {
        this.errorMessage = null;
        this.successMessage= null;
        this.isManualFormChecked = true;
        this.isDynamicFormChecked = false;
    }

    changeView($event, item: Number) {
        this.successMessage = null;
        this.errorMessage = null;
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
        this.successMessage = null;
        this.errorMessage = null;
        this.eventsToAdd.push(EventRequest);
    }

    addEventTempDynamic(eventsRequest: EventRequestDynamic) {
        this.successMessage = null;
        this.errorMessage = null;

        eventsRequest.userName = this.sessionService.getCurrentUserName();
        // buscar los eventos a crear.
        this.fixtureService.getGenerateFixtures(eventsRequest).subscribe(response => {
            //this.auxEventFromDynamic = ;
            if(this.eventsToAdd == null) this.eventsToAdd = [];
            response.forEach(event => {
                this.auxEvent = { sportName: null, eventDate: null, teamNames: []};
                
                this.auxEvent.eventDate = event.initialDate;
                this.auxEvent.sportName = event.sportName;
                this.auxEvent.teamNames = [];
                event.teams.forEach(tem => { this.auxEvent.teamNames.push(tem.name) });
    
                this.eventsToAdd.push(this.auxEvent);
            });
          },
          error => this.handleErrorDynamic(error));

        
    }

    deleteEvent($event, event: EventRequest) {
        const index: number = this.eventsToAdd.indexOf(event);
        if (index !== -1) {
            this.eventsToAdd.splice(index, 1);
        }
    }

    onSubmit() {
        
        this.successMessage = null;
        this.errorMessage = null;
        this.eventsToAdd.forEach(element => {
            this.eventService.addEvent(element).subscribe(
                response => this.handleResponse(response, element),
                error => this.handleError(error, element));
        });
        this.eventsToAdd = null;
    }

    private handleResponse(response: any, element: EventRequest) {
        if(this.successMessage == null) this.successMessage = '';
        this.successMessage += 'Operation success for element ' + element.teamNames + "<br/>";
    }

    private handleError(error: any, element: EventRequest) {
        if(this.errorMessage == null) this.errorMessage = '';
        this.errorMessage += 'The event : ' + element.teamNames + ' has error : ' + error.error + "<br/>";
    }

    private handleErrorDynamic(error: any) {
        this.errorMessage = error.error;
    }

}