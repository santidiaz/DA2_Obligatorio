import { DatePipe } from "@angular/common";

export interface EventRequest {
    eventDate: Date | DatePipe;
    teamNames: Array<string>;
    sportName: string;
}