import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { Component, EventEmitter, Output } from "@angular/core";
import { SportRequest } from "src/app/sportsManager/interfaces/sportrequest";
import { TeamRequest } from "src/app/sportsManager/interfaces/team-request";
import { EventRequest } from "src/app/sportsManager/interfaces/event-request";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { TeamService } from "src/app/sportsManager/services/team.service";
import { SportService } from "src/app/sportsManager/services/sport.service";
import { EventRequestDynamic } from "src/app/sportsManager/interfaces/event-request-dynamic";
import { FixtureService } from "src/app/sportsManager/services/fixture.services";

@Component({
    selector: 'app-addeventdynamic',
    templateUrl: './addeventdynamic.component.html',
    styleUrls: ['./addeventdynamic.component.css']
  })
  
  export class AddEventDynamicComponent extends BaseComponent {

    sports: Array<SportRequest>;
    fixtures: Array<string>;
    fromModel: EventRequestDynamic;

    @Output() closeRequested = new EventEmitter<EventRequestDynamic>();

    constructor(
      private sessionService: SessionService,
      private sportService: SportService,
      private fixtureService: FixtureService) {
      super();
      this.clearFromModel();
    };

    ngOnInit() {
      
      this.sportService.getSports().subscribe(response => {
        this.sports = response;
      });

      this.fixtureService.getFixtures().subscribe(response => {
        this.fixtures = response;
      });
      
    }

    addEvent() {
      this.closeRequested.emit(this.fromModel);
      this.clearFromModel();
    }
  
    clearFromModel(){
      this.fromModel = { 
        initialDate: null,
        fixtureName: null,
        sportName: null};
    }

}