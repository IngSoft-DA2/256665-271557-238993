import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConstructionCompanyCreateRequest } from '../interfaces/construction-company-create-request';
import { environment } from '../../../../environments/environment.development';
import { ConstructionCompanyCreateResponse } from '../interfaces/construction-company-create-response';
import { Observable } from 'rxjs';
import { ConstructionCompany } from '../interfaces/construction-company';
import { ConstructionCompanyUpdateRequest } from '../interfaces/construction-company-update-request';

@Injectable({
  providedIn: 'root'
})
export class ConstructionCompanyService {

  constructor(private http: HttpClient) { }

  createConstructionCompany(constructionCompanyToCreate: ConstructionCompanyCreateRequest): Observable<ConstructionCompanyCreateResponse> {
    return this.http.post<ConstructionCompanyCreateResponse>(`${environment.apiBaseUrl}/api/v2/construction-companies?addAuth=true`, constructionCompanyToCreate)
  }

  updateConstructionCompany(id: string, constructionCompanyToUpd: ConstructionCompanyUpdateRequest): Observable<void> {
    return this.http.put<void>(`${environment.apiBaseUrl}/api/v2/construction-companies/${id}?addAuth=true`, constructionCompanyToUpd);
  }
  getConstructionCompanyById(idOfConstructionCompany: string): Observable<ConstructionCompany> {
    return this.http.get<ConstructionCompany>(`${environment.apiBaseUrl}/api/v2/construction-companies/${idOfConstructionCompany}?addAuth=true`);
  }

  getConstructionCompanyByUserCreatorId(userId: string) {
    return this.http.get<ConstructionCompany>(`${environment.apiBaseUrl}/api/v2/user-id/${userId}/construction-companies?addAuth=true`);
  }


}
