import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { EventService } from '../../../services/event.service'
import { Event } from '../../../interfaces/event'
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { TeamService } from 'src/app/sportsManager/services/team.service';
import { SportService } from 'src/app/sportsManager/services/sport.service';


@Component({
    selector: 'app-event-list-by-param',
    templateUrl: './eventListByParam.component.html',
    styleUrls: ['./eventListByParam.component.css']
})
export class EventListByParamComponent extends BaseComponent {

    private errorMessage: any;// TODO: Ver que retorna y crear una interfaz con eso. codigo y mensaje ?
    protected events: Array<Event>;
    protected eventTypeTitle: string = 'Teams';
    successMessage: string = null;
    title: string = null;

    nameParam: string;
    itemParam: string;

    constructor(private eventService: EventService,
        private router: ActivatedRoute,
        private teamService: TeamService,
        private sportServices: SportService,
        private routerAux: Router) {
        super();
        this.nameParam = null;
        this.itemParam = null;
    }

    ngOnInit() {

        if (this.router != null) {
            this.nameParam = this.router.snapshot.paramMap.get('name');
            this.itemParam = this.router.snapshot.paramMap.get('item');

            this.updateGrid();
            this.successMessage = null;
        }

    }

    componentOnInit() {
        this.updateGrid();
    }

    updateGrid() {
        if (this.itemParam == '1') {
            this.teamService.getEventsByTeam(this.nameParam).subscribe(
                response => this.handleResponse(response),
                error => this.handleError(error));

            this.title = 'Report of events for team ' + this.nameParam;
        }
        else {
            this.sportServices.getEventsBySport(this.nameParam).subscribe(
                response => this.handleResponse(response),
                error => this.handleError(error));

            this.title = 'Report of events for sport ' + this.nameParam;
        }
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
        return this.events != undefined && this.events.length > 0;
    }
    
    return() {

        if (this.itemParam == '1') {
            this.routerAux.navigate(['/listTeams']);
        }
        else {
            this.routerAux.navigate(['/listSport']);
        }
    }

}