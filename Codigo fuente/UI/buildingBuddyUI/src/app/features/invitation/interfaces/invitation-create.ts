import { SystemUserRoleEnum } from "./enums/system-user-role-enum";

export interface invitationCreateRequest
{
    firstname : string;
    lastname : string;
    email: string;
    expirationDate : Date;
    role : SystemUserRoleEnum
}