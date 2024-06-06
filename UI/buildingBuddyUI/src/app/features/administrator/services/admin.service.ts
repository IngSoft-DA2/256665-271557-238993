import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AdminCreateRequest } from '../interfaces/admin-create-request';
import { AdminCreateResponse } from '../interfaces/admin-create-response';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  createAdministrator(adminToCreate: AdminCreateRequest)
  {
    return this.http.post<AdminCreateResponse>(`${environment.apiBaseUrl}/api/v2/administrators?addAuth=true`,adminToCreate)
  }




}
