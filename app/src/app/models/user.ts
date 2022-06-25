export interface User {
    userId: string;
    userRoles: Array<string>;
    claims: Array<any>;
    identityProvider: string,
    userDetails: string
}
