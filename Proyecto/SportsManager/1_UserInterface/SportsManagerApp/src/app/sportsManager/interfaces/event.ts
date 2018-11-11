
export interface Event {
    id: number;
    initialDate: Date;
    multipleTeams: boolean;
    teams: Array<any>;
    result: Array<any>;
    comments: Array<any>;    
    sportId: number;
    sportName: string;
}