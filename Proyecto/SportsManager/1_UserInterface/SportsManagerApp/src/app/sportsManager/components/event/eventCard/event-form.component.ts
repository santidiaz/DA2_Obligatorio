import { Component, Input } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { EventService } from '../../../services/event.service'
import { Event } from '../../../interfaces/event'


@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.css']
})
export class EventFormComponent extends BaseComponent {

  @Input()
  protected event: Event;

  private errorMessage: any;// TODO: Ver que retorna y crear una interfaz con eso. codigo y mensaje ?
  protected events: Array<Event>;


  constructor(private eventService: EventService) {
    super();
  }





  /*
     componentOnInit(){
      this.eventService.getAllEvents().subscribe(
        response => this.handleResponse(response), 
        error => this.handleError(error));
     }
  
     private handleResponse(response: any){
        let algo = response;
  
  
  
     }
  
     private handleError(error: any){
      let algo = error;
   }
  
   */
}