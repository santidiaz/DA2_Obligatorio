import { TeamResult } from "./team-result";

export interface EventResult {
    id: number;
    teamsResult: Array<TeamResult>;
}