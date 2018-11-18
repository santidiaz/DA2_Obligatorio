import { BaseComponent } from "src/app/sportsManager/shared/base.component";
import { Component, Output, EventEmitter, Input } from "@angular/core";
import { SessionService } from "src/app/sportsManager/services/session.service";
import { TeamService } from "src/app/sportsManager/services/team.service";
import { SportService } from "src/app/sportsManager/services/sport.service";
import { Event } from '../../../interfaces/event'
import { Router } from "@angular/router";

@Component({
    selector: 'app-event-details',
    templateUrl: './event-details.component.html',
    styleUrls: ['./event-details.component.css']
})
export class EventDetailsComponent extends BaseComponent {

    //sports: Array<SportRequest>;
    //teams: Array<TeamRequest>;
    //fromModel: EventRequest;

    _event: Event;

    @Input()
    set event(value: Event) {
        this._event = value;
    }
    get event(): Event {
        return this._event;
    }

    @Output() closeRequested = new EventEmitter<any>();

    constructor(
        private sessionService: SessionService,
        private teamService: TeamService,
        private sportService: SportService,
        private route: Router) {
        super();
        //this.clearFromModel();
    };

    closeDetails(){
        this._event = undefined;
        this.closeRequested.emit(true);
    }

    componentOnInit() {
        // from https://v6.angular.io/guide/router
        /* this.route.paramMap.pipe(
             switchMap((params: ParamMap) => {
                 // (+) before `params.get()` turns the string into a number
                 const cityId = +params.get('id');
                 console.log(`param id value: ${cityId}`);
                 return this.cityService.getCity(cityId);
             })
         ).subscribe(x => x ? this.updateForm(x) : this.cityForm.reset());
 
         console.log(`Using snapshot : ${this.route.snapshot.paramMap.get('id')}`);
 
         this.updateForm(this.city);*/
    }

    /*clearFromModel() {
        this.fromModel = {
            eventDate: null,
            teamNames: null,
            sportName: null
        };
    }*/


}