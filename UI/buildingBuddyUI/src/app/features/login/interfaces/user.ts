import { SystemUserRoleEnum } from "../../invitation/interfaces/enums/system-user-role-enum";

export interface User
{
    email : string,
    password : string,
    sessionString : string,
    userRole : SystemUserRoleEnum
};