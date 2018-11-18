import { DatePipe } from "@angular/common";

export interface Comment {
    id: number;
    description: string;
    creatorName: string;
    datePosted: Date | DatePipe;
}