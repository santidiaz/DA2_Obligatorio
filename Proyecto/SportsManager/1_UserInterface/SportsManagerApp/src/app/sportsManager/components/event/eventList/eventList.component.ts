import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { SessionService } from '../../../services/session.service'

@Component({
  selector: 'app-eventList',
  templateUrl: './eventList.component.html',
  styleUrls: ['./eventList.component.scss']
})
export class EventListComponent extends BaseComponent {

  errorMessage: any;// TODO: Ver que retorna y crear una interfaz con eso. codigo y mensaje ?

  constructor(private _session: SessionService) {
    super();
   }
}