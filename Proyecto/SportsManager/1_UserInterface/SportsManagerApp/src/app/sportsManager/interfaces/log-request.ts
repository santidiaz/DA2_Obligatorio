import { DatePipe } from "@angular/common";

export interface LogRequest {
    fromDate: Date | DatePipe;
    toDate: Date | DatePipe;
}