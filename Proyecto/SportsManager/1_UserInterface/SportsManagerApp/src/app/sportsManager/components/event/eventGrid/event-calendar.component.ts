import {
  Component,
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef
} from '@angular/core';
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours
} from 'date-fns';
import { Subject } from 'rxjs';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarView
} from 'angular-calendar';
import { BaseComponent } from 'src/app/sportsManager/shared/base.component';
import { SportRequest } from 'src/app/sportsManager/interfaces/sportrequest';
import { SportService } from 'src/app/sportsManager/services/sport.service';
import { Event } from 'src/app/sportsManager/interfaces/event';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  }
};

@Component({
  selector: 'app-events-calendar',
  styleUrls: ['./event-calendar.component.css'],
  templateUrl: './event-calendar.component.html'
})
export class EventsCalendar extends BaseComponent {

  view: CalendarView = CalendarView.Month;
  CalendarView = CalendarView;
  viewDate: Date = new Date();
  refresh: Subject<any> = new Subject();
  activeDayIsOpen: boolean = false;
  calendarEvents: CalendarEvent[];

  sports: Array<SportRequest> = [];
  sportEvents: Array<Event> = [];
  showCalendar: boolean = false;

  constructor(private sportService: SportService) {
    super();
  }

  componentOnInit() {
    this.sportService.getSports().subscribe(response => {
      this.sports = response;
    });
  }

  onChange(selectedValue: SportRequest) {
    this.sportService.getSportEvents(selectedValue.name).subscribe(response => {
      this.sportEvents = response;
      this.loadCalendarEvents();
      this.showCalendar = true;
    });
  }

  loadCalendarEvents() {

    this.sportEvents.forEach((element: Event) => {
      this.events.push({
        title: this.showTeams(element),
        start: startOfDay(<any>element.initialDate),
        end: endOfDay(<any>element.initialDate),
        color: colors.red,
        draggable: true,
        resizable: {
          beforeStart: true,
          afterEnd: true
        }
      });
      this.refresh.next();


    });
  }





  showTeams(eventData: Event) : string {
    let result: string = "";

    if(eventData.allowMultipleTeams){
      eventData.teams.forEach(team => {
        result += team.name + '\n';
      });

    } else {
      result = eventData.teams[0].name + ' vs ' + eventData.teams[1].name;
    }
    
    return result;
  }


  actions: CalendarEventAction[] = [
    {
      label: '<i class="fa fa-fw fa-pencil"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      }
    },
    {
      label: '<i class="fa fa-fw fa-times"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.events = this.events.filter(iEvent => iEvent !== event);
        this.handleEvent('Deleted', event);
      }
    }
  ];

  events: CalendarEvent[] = [
    /*{
      start: subDays(startOfDay(new Date()), 1),
      end: addDays(new Date(), 1),
      title: 'A 3 day event',
      color: colors.red,
      actions: this.actions,
      allDay: true,
      resizable: {
        beforeStart: true,
        afterEnd: true
      },
      draggable: true
    },
    {
      start: startOfDay(new Date()),
      title: 'An event with no end date',
      color: colors.yellow,
      actions: this.actions
    },
    {
      start: subDays(endOfMonth(new Date()), 5),
      end: addDays(endOfMonth(new Date()), 5),
      title: 'A long event that spans 2 months',
      color: colors.blue,
      allDay: true
    },
    {
      start: addHours(startOfDay(new Date()), 2),
      end: new Date(),
      title: 'A draggable and resizable event',
      color: colors.yellow,
      actions: this.actions,
      resizable: {
        beforeStart: true,
        afterEnd: true
      },
      draggable: true
    }*/
  ];






  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      this.viewDate = date;
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd
  }: CalendarEventTimesChangedEvent): void {
    event.start = newStart;
    event.end = newEnd;
    this.handleEvent('Dropped or resized', event);
    this.refresh.next();
  }

  handleEvent(action: string, event: CalendarEvent): void {
    //this.modalData = { event, action };      
  }

  addEvent(): void {
    this.events.push({
      title: 'New event',
      start: startOfDay(new Date()),
      end: endOfDay(new Date()),
      color: colors.red,
      draggable: true,
      resizable: {
        beforeStart: true,
        afterEnd: true
      }
    });
    this.refresh.next();
  }
}