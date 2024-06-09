import { Flat } from "../../Building/Interfaces/Flat.model";
import { StatusEnum } from "../../invitation/interfaces/enums/status-enum";
import { RequestHandler } from "../../requestHandler/interfaces/RequestHandler.model";
import { MaintenanceStatusEnum } from "./enums/maintenance-status-enum";

export interface MaintenanceRequest {
    id: string;
    description: string;
    flatId: string;
    flat: Flat;
    category: string;
    openedDate:  Date | null;
    closedDate: Date | null;
    requestHandlerId: string; 
    requestHandler: RequestHandler;
    requestStatus: MaintenanceStatusEnum;
}
