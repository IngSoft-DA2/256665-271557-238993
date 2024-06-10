import { StatusEnum } from "./enums/status-enum";
import { SystemUserRoleEnum } from "./enums/system-user-role-enum";

export interface Invitation
{
    id : string;
    firstname : string;
    lastname : string;
    email : string;
    expirationDate : Date;
    status : StatusEnum;
    role: SystemUserRoleEnum;
}

