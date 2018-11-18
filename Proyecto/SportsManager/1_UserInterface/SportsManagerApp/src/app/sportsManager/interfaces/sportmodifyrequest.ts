export interface SportModifyRequest {
    sportOID: number;
    oldName: string;
    newName: string;
    multipleTeamsAllowed: boolean;
}