import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MaintenanceCreateResponse } from '../Interfaces/maintenance-create-response';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { MaintenanceCreateRequest } from '../Interfaces/maintenance-create-request';

@Injectable({
  providedIn: 'root'
})
export class MaintenanceRequestService {

  constructor(private http: HttpClient) { }

  createMaintenanceRequest(maintenanceToCreate: MaintenanceCreateRequest): Observable<MaintenanceCreateResponse> {
      return this.http.post<MaintenanceCreateResponse>(`${environment.apiBaseUrl}/api/v2/maintenance`, maintenanceToCreate);
  }

  
}
