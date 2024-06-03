import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MaintenanceRequest } from '../../MaintenanceRequest/Interfaces/maintenanceRequest.model';
import { NodeReportMaintenanceRequestsByBuilding } from '../report-maintenance-requests-by-building/interfaces/node-report-maintenance-requests-by-building';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient) { }

  getReportMaintenanceRequestsByBuilding(buildingIdSelected?: string) : NodeReportMaintenanceRequestsByBuilding[]{
    return this.http.get<MaintenanceRequest[]>(`${environment.apiBaseUrl}/maintenance`);
  }
}
