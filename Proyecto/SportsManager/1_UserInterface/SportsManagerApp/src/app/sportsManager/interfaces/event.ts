import { Team } from "./team";
import { Comment } from "./comment";
import { DatePipe } from "@angular/common";

export interface Event {
    id: number;
    initialDate: Date | DatePipe;
    allowMultipleTeams: boolean;
    teams: Array<Team>;
    result: Array<any>;
    comments: Array<Comment>;    
    sportId: number;
    sportName: string;
}