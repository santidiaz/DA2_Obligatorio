export interface User {
    userOID: number;
    token: string;
    name: string;
    lastName: string;
    userName: string;
    email: string;
    isAdmin: boolean;
    //FavouriteTeams: Array<Team>
}