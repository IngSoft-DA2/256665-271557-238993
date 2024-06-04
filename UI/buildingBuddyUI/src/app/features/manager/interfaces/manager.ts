import { Building } from "../../building/interfaces/building";
import { SystemUserRoleEnum } from "../../invitation/interfaces/enums/system-user-role-enum";
import { MaintenanceRequest } from "../../maintenanceRequest/interfaces/maintenanceRequest";

export interface Manager
{
    id : string,
    firstname : string,
    email: string,
    password : string,
    role : SystemUserRoleEnum.Manager,
    buildings : Building[],
    requests : MaintenanceRequest[]
};