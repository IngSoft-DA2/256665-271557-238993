import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MaintenanceRequest } from '../../MaintenanceRequest/Interfaces/maintenanceRequest.model';
import { NodeReportMaintenanceRequestsByBuilding } from '../report-maintenance-requests-by-building/interfaces/node-report-maintenance-requests-by-building';
import { Observable, of } from 'rxjs';
import { environment } from '../../../../environments/environment';

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
}
