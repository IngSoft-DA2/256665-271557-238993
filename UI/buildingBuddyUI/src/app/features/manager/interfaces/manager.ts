import { SystemUserRoleEnum } from "../../invitation/interfaces/enums/system-user-role-enum";
import { MaintenanceRequest } from "../../maintenanceRequest/interfaces/maintenanceRequest";

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