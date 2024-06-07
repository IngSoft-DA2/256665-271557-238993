import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MaintenanceRequest } from '../../MaintenanceRequest/Interfaces/maintenanceRequest.model';
import { NodeReportMaintenanceRequestsByBuilding } from '../interfaces/node-report-maintenance-requests-by-building';
import { Observable, of } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { NodeReportMaintenanceRequestsByRequestHandler } from '../interfaces/node-report-maintenance-req-by-req-handler';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient) { }

  getReportMaintenanceRequestsByBuilding(userId: string, buildingIdSelected: string): Observable<NodeReportMaintenanceRequestsByBuilding[]> {

      const params = new HttpParams()
        .set('managerId', userId)
        .set('buildingId', buildingIdSelected);

      return this.http.get<NodeReportMaintenanceRequestsByBuilding[]>(`${environment.apiBaseUrl}/api/v2/buildings/maintenance-requests/reports`, {params});
  }

  getReportMaintenanceRequestsByRequestHandler(userId: string, buildingIdSelected: string, requestHandlerId: string): Observable<NodeReportMaintenanceRequestsByRequestHandler[]> {

    const params = new HttpParams()
    .set('requestHandlerId', requestHandlerId)
    .set('buildingId', buildingIdSelected)
    .set('managerId', userId);

    return this.http.get<NodeReportMaintenanceRequestsByRequestHandler[]>(`${environment.apiBaseUrl}/api/v2/request-handler/maintenance-requests/reports`, {params});
  }
}
