import { TeamRequest } from "./team-request";

export interface SportRequest {
    id: number;
    name: string;
    multipleTeamsAllowed: boolean;
    teams: Array<TeamRequest>;
}