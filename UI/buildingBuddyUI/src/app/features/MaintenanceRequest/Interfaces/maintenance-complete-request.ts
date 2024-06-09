import { StatusEnum } from "../../invitation/interfaces/enums/status-enum";
import { MaintenanceStatusEnum } from "./enums/maintenance-status-enum";

export interface MaintenanceCompleteRequest {
    requestStatus: MaintenanceStatusEnum;
}