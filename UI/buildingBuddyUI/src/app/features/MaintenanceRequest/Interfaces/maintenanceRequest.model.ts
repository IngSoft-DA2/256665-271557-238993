import { Flat } from "../../flat/interfaces/flat";
import { StatusEnum } from "../../invitation/interfaces/enums/status-enum";
import { RequestHandler } from "../../requestHandler/interfaces/RequestHandler.model";
import { MaintenanceStatusEnum } from "./enums/maintenance-status-enum";

export interface MaintenanceRequest {
    id: string;
    description: string | null;
    flatId: string;
    flat: Flat;
    category: string;
    openedDate:  Date | null;
    closedDate: Date | null;
    requestHandlerId: string | null; 
    requestHandler: RequestHandler | null;
    requestStatus: MaintenanceStatusEnum;
}
