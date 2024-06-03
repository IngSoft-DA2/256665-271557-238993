import { Injectable } from '@angular/core';
import { ManagerCreateRequest } from '../interfaces/manager-create-request';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { ManagerCreateResponse } from '../interfaces/manager-create-response';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {

  constructor(private http : HttpClient) { }

  createManager(managerToCreate: ManagerCreateRequest) : Observable<ManagerCreateResponse>
  {
    return this.http.post<ManagerCreateResponse>(`${environment.apiBaseUrl}/api/v2/managers`,managerToCreate);
  }




}
