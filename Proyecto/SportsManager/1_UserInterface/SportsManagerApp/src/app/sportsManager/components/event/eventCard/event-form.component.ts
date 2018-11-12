import { Component, Input, SimpleChanges } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { EventService } from '../../../services/event.service'
import { Event } from '../../../interfaces/event'
import { FormGroup, Validators, FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.css']
})
export class EventFormComponent extends BaseComponent {

  private _isAdmin: boolean;
  private _event: Event;

  @Input()
  set event(value: Event) {
    this._event = value;
    this.updateForm(value);
  }
  get event(): Event {
    return this._event;
  }

  @Input()
  set isAdmin(value: boolean) {
    this._isAdmin = value;
  }
  get isAdmin(): boolean {
    return this._isAdmin;
  }

  @Input()
  protected editable: boolean = true;


  errorMessage: string;
  //eventForm: FormGroup;

  //@Output() closeRequested = new EventEmitter<boolean>();


  constructor(private formBuilder: FormBuilder, private eventService: EventService) {
    super();

    this._event = {
      id: 0,
      initialDate: new Date,
      sportName: '',
      sportId: 0,
      allowMultipleTeams: false,
      comments: [],
      result: [],
      teams: [],
    }
    /*this.eventForm = this.formBuilder.group();*/
  }

  componentOnInit() {
  }


  updateForm(event: Event) {
    /* this.eventForm.patchValue({
       id: event.id,
       initialDate: event.initialDate,
       sportName: event.sportName,
       sportId: event.sportId,
       multipleTeams: event.multipleTeams,
       comments: event.comments,
       result: event.result,
       teams: event.teams,
     });*/
  }

  componentOnChanges(changes: SimpleChanges) {
    // changes.prop contains the old and the new value...
    if (changes.event.currentValue) {
      const ev = changes.event.currentValue;
      /*this.eventForm.patchValue({
        id: ev.id,
        initialDate: ev.initialDate,
        sportName: ev.sportName,
        sportId: ev.sportId,
        multipleTeams: ev.multipleTeams,
        comments: ev.comments,
        result: ev.result,
        teams: ev.teams,
      });
    }*/
    }
  }


  onSubmit() {
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