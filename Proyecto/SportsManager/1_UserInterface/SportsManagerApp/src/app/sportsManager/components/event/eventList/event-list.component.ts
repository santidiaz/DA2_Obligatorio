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


  constructor(private eventService: EventService) {
    super();
   }

   componentOnInit(){
    this.eventService.getAllEvents().subscribe(
      response => this.handleResponse(response), 
      error => this.handleError(error));
   }

   private handleResponse(response: any){
    this.events = <Array<Event>>response;



   }

   private handleError(error: any){
     console.log(error);
    let algo = error;
 }

  selectedEvent: Event;
  isFormActive: boolean;

  selectCity($event, event: Event) {
    this.selectedEvent = event;
    this.isFormActive = true;
  }
/*
  deleteCity($event, city: City) {
    this.cityService.deleteCity(city.id).subscribe(resp => {
      console.log(JSON.stringify(resp));
      this.updateGrid();
    });
  }*/

  closeForm($event) {
    this.isFormActive = false;
  }



}