import { Team } from "./team";
import { Comment } from "./comment";
import { DatePipe } from "@angular/common";
import { EventResult } from "./event-result";

export interface Event {
    id: number;
    initialDate: Date | DatePipe;
    teams: Array<Team>;
    comments: Array<Comment>;    
    allowMultipleTeams: boolean;
    sportId: number;
    sportName: string;
    teamsString: Array<string>;
    result: EventResult;
}