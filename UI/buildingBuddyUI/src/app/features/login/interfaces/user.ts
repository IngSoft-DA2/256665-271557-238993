import { SystemUserRoleEnum } from "../../invitation/interfaces/enums/system-user-role-enum";

export interface User
{
    userId : string,
    email : string,
    password : string,
    sessionString : string,
    userRole : SystemUserRoleEnum
};