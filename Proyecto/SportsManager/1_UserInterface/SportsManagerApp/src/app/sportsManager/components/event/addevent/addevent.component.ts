import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { Component } from "@angular/core";
import { EventRequest } from "src/app/sportsManager/interfaces/event-request";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { EventService } from "src/app/sportsManager/services/event.service";

@Component({
    selector: 'app-addevent',
    templateUrl: './addevent.component.html',
    styleUrls: ['./addevent.component.css']
  })
  
  export class AddEventComponent extends BaseComponent {

    isManualFormChecked: boolean;
    isDynamicFormChecked: boolean;
    eventsToAdd: Array<EventRequest> = [];

    constructor(
        private sessionService: SessionService,
        private eventService: EventService) {
        super();;
      };

    ngOnInit() {
        //this.updateGrid();
        //this.successMessage = null;
        this.isManualFormChecked = true;
        this.isDynamicFormChecked = false;
    }

    changeView($event, item: Number) {
        if(item == 1)
        {
            this.isManualFormChecked = true;
            this.isDynamicFormChecked = false;
        }
        else 
        {
            this.isManualFormChecked = false;
            this.isDynamicFormChecked = true;
        }
      }

      addEventTemp(EventRequest: EventRequest) {

        this.eventsToAdd.push(EventRequest);
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