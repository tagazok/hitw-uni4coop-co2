export interface User {
    userUI: string;
    userRoles: Array<string>;
    claims: Array<any>;
    identityProvider: string,
    userDetails: string
}
