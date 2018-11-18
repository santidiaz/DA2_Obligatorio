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
  showEventDetails: boolean = false;
  selectedEvent: Event;

  constructor(private sportService: SportService) {
    super();
  }

  componentOnInit() {
    this.sportService.getSports().subscribe(response => {
      this.sports = response;
    });
    this.selectedEvent = undefined;
  }

  onChange(selectedValue: SportRequest) {
    this.calendarEvents = [];
    this.showEventDetails = false;
    this.activeDayIsOpen = false;

    this.sportService.getSportEvents(selectedValue.name).subscribe(response => {
      this.sportEvents = response;
      this.loadCalendarEvents();
      this.showCalendar = true;
    });
  }

  loadCalendarEvents() {
    this.sportEvents.forEach((element: Event) => {
      this.calendarEvents.push({
        title: this.showTeams(element),
        start: startOfDay(<any>element.initialDate),
        end: endOfDay(<any>element.initialDate),
        id: element.id,
        color: colors.red,
        draggable: false,
        resizable: {
          beforeStart: true,
          afterEnd: true
        }
      });
      this.refresh.next();
    });
  }

  showTeams(eventData: Event): string {
    let result: string = "";

    if (eventData.allowMultipleTeams) {
      eventData.teams.forEach(team => {
        result += team.name + ' | ';
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
        this.calendarEvents = this.calendarEvents.filter(iEvent => iEvent !== event);
        this.handleEvent('Deleted', event);
      }
    }
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
    this.showCalendar = false;
    this.showEventDetails = true;
    this.activeDayIsOpen = false;

    this.selectedEvent = this.sportEvents.find(se => se.id == event.id);
  }

  closeDetails($event) {
    this.showCalendar = true;
    this.activeDayIsOpen = false;
    this.showEventDetails = false;
  }
}