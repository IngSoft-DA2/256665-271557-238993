import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConstructionCompanyCreateRequest } from '../interfaces/construction-company-create-request';
import { environment } from '../../../../environments/environment.development';
import { ConstructionCompanyCreateResponse } from '../interfaces/construction-company-create-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConstructionCompanyService {

  constructor(private http : HttpClient) { }

  createConstructionCompany(constructionCompanyToCreate : ConstructionCompanyCreateRequest) : Observable<ConstructionCompanyCreateResponse> 
  {
    return this.http.post<ConstructionCompanyCreateResponse>(`${environment.apiBaseUrl}/api/v2/construction-companies`,constructionCompanyToCreate)
  }
}
