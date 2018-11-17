import { DatePipe } from "@angular/common";

export interface Log {
    action: string;
    logDate: Date | DatePipe;
    userName: string;
}