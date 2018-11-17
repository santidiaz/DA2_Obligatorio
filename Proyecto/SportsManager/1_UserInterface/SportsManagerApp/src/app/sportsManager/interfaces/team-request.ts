export interface TeamRequest {
    teamOID: number;
    name: string;
    sportOID: number;
    photo: File;
    isFavorite: Boolean;
    photoString: string;
}