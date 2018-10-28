export interface User {
    userOID: number;
    token: string;
    name: string;
    lastName: string;
    userName: string;
    email: string;
    isAdmin: boolean;
    //public virtual List<UserTeam> FavouriteTeams { get; set; }
}