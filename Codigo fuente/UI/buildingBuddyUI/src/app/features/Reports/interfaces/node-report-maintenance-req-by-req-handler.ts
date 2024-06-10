export interface NodeReportMaintenanceRequestsByRequestHandler {
    openRequests: string;
    closedRequests: string;
    onAttendanceRequests: number; 
    requestHandlerId: string;
    averageTimeToCloseRequest: string;
}