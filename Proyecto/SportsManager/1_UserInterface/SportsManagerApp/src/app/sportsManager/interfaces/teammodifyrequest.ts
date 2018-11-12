export interface TeamModifyRequest {
    teamOID: number;
    newName: string;
    oldName: string;
    photo: File;
}