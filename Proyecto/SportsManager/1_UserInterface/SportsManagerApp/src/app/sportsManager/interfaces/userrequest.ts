export interface UserRequest {
    userOID: number;
    token: string;
    name: string;
    lastName: string;
    userName: string;
    email: string;
    isAdmin: boolean;
}