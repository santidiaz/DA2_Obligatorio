import { Team } from "./team";
import { Comment } from "./comment";
import { DatePipe } from "@angular/common";

export interface Event {
    id: number;
    initialDate: Date | DatePipe;
    teams: Array<Team>;
    comments: Array<Comment>;    
    allowMultipleTeams: boolean;
    sportId: number;
    sportName: string;
    result: Array<any>;
}