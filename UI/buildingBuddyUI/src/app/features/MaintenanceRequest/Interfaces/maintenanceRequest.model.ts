import { Building } from "../../Building/Interfaces/Building.model";

export interface MaintenanceRequest {
    id: string;
    description: string;
    //flat: Flat;
    //status: StatusEnum;
    openedDate:  string;
    closedRequests: string;
    onAttendanceRequests: number; 
    buildingId: string; 
}
