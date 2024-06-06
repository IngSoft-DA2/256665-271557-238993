import { SystemUserRoleEnum } from "../../invitation/interfaces/enums/system-user-role-enum"

export interface LoginResponse
{
    userId : string
    sessionString : string,
    userRole : SystemUserRoleEnum
};