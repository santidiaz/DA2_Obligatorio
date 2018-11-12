import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { EventService } from '../../../services/event.service'
import { Event } from '../../../interfaces/event'


@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']
})
export class EventListComponent extends BaseComponent {

  private errorMessage: any;// TODO: Ver que retorna y crear una interfaz con eso. codigo y mensaje ?
  protected events: Array<Event>;
  protected eventTypeTitle: string = 'Teams';
  successMessage: string = null;

  constructor(private eventService: EventService) {
    super();
  }

  ngOnInit() {
    this.updateGrid();
    this.successMessage = null;
}

  componentOnInit() {
    this.updateGrid();
  }

  updateGrid() {
    this.eventService.getAllEvents().subscribe(
      response => this.handleResponse(response),
      error => this.handleError(error));
  }

  private handleResponse(response: any) {
    this.events = <Array<Event>>response;
  }

  private handleError(error: any) {
    console.log(error);
    let algo = error;
  }

  selectedEvent: Event;
  isFormActive: boolean;


  get existsEvents(): boolean {
    return this.events !== undefined && this.events.length > 0;
  }

  selectCity($event, event: Event) {
    this.selectedEvent = event;
    this.isFormActive = true;
  }
  
  deleteEvent($event, event: Event) {
    this.eventService.deleteEvent(event.id).subscribe(resp => {
        console.log(JSON.stringify(resp));
        this.successMessage = 'Operation success';
        this.updateGrid();
    });
}

  closeForm($event) {
    this.isFormActive = false;
  }



}