import { Injectable } from '@angular/core';
import { ManagerCreateRequest } from '../interfaces/manager-create-request';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { ManagerCreateResponse } from '../interfaces/manager-create-response';
import { Manager } from '../interfaces/manager';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {

  constructor(private http : HttpClient) { }

  createManager(managerToCreate: ManagerCreateRequest, idOfInvitationAccepted : string) : Observable<ManagerCreateResponse>
  {
    const params = new HttpParams().set('idOfInvitationAccepted', idOfInvitationAccepted);
    return this.http.post<ManagerCreateResponse>(`${environment.apiBaseUrl}/api/v2/managers`,managerToCreate, {params});
  }

  getAllManagers() : Observable<Manager[]>
  {
    return this.http.get<Manager[]>(`${environment.apiBaseUrl}/api/v2/managers?addAuth=true`);
  }

  getManagerById(managerId: string) : Observable<Manager>
  {
    return this.http.get<Manager>(`${environment.apiBaseUrl}/api/v2/managers/${managerId}?addAuth=true`);
  }

  deleteManager(managerId: string)  : Observable<void>
  {
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/v2/managers?addAuth=true/${managerId}?addAuth=true`)
  }




}
