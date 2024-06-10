import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { constructionCompanyAdminCreateRequest } from '../interfaces/construction-company-admin-create-request';
import { environment } from '../../../../environments/environment';
import { ConstructionCompanyAdminCreateResponse } from '../interfaces/construction-company-admin-create-response';
import { ConstructionCompanyAdmin } from '../interfaces/construction-company-admin';

@Injectable({
  providedIn: 'root'
})
export class ConstructionCompanyAdminService
{

  constructor(private http : HttpClient) { }

  createConstructionCompanyAdmin(createRequest: constructionCompanyAdminCreateRequest)
  {
    return this.http.post<ConstructionCompanyAdminCreateResponse>(`${environment.apiBaseUrl}/api/v2/ConstructionCompanyAdmins?addAuth=true`,createRequest);
  }

}
