export interface TeamModifyRequest {
    teamOID: number;
    newName: string;
    oldName: string;
    photo: File;
    newPhoto: File;
    photoString: string;
    sportOID: number;
}