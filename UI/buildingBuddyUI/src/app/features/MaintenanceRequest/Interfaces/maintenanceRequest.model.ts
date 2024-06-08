import { StatusEnum } from "../../invitation/interfaces/enums/status-enum";

export interface MaintenanceRequest {
    id: string;
    description: string;
    flatId: string;
    category: string;
    openedDate:  string;
    closedDate: string;
    requestHandlerId: string; 
    requestStatus: StatusEnum;
}
