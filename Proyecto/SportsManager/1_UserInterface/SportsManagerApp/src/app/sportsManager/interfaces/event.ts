import { Team } from "./team";
import { DatePipe } from "@angular/common";

export interface Event {
    id: number;
    initialDate: Date | DatePipe;
    allowMultipleTeams: boolean;
    teams: Array<Team>;
    result: Array<any>;
    comments: Array<any>;    
    sportId: number;
    sportName: string;
}