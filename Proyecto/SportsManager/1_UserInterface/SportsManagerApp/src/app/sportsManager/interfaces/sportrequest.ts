import { TeamRequest } from "./team-request";

export interface SportRequest {
    sportOID: number;
    name: string;
    multipleTeamsAllowed: boolean;
    teams: Array<TeamRequest>;
}