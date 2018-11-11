export interface SportModifyRequest {
    sportOID: number;
    oldName: string;
    newName: string;
    allowdMultipleTeamsEvents: boolean;
}