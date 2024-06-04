import { SystemUserRoleEnum } from "../../invitation/interfaces/enums/system-user-role-enum";

export interface Manager
{
    id : string,
    name : string,
    email: string,
    password : string,
    role : SystemUserRoleEnum.Manager,
    buildings : string[],
    requests : string[]
};