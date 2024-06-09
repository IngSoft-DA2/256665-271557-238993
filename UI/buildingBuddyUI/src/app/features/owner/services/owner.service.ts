import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Owner } from '../interfaces/owner';
import { environment } from '../../../../environments/environment.development';
import { OwnerCreateResponse } from '../interfaces/owner-create-response';
import { OwnerCreateRequest } from '../interfaces/owner-create-request';
import { OwnerUpdateRequest } from '../interfaces/owner-update-request';

@Injectable({
  providedIn: 'root'
})
export class OwnerService {

  constructor(private http: HttpClient) { }

  getOwnerById(ownerId : string) : Observable<Owner>
  {
    return this.http.get<Owner>(`${environment.apiBaseUrl}/api/v2/owners/${ownerId}`);
  }

  getOwners() : Observable<Owner[]>
  {
    return this.http.get<Owner[]>(`${environment.apiBaseUrl}/api/v2/owners`);
  }

  createOwner(ownerToCreate: OwnerCreateRequest) : Observable<OwnerCreateResponse>
  {
    return this.http.post<OwnerCreateResponse>(`${environment.apiBaseUrl}/api/v2/owners`,ownerToCreate);
  }

  updateOwner(id : string, ownerWithUpdates : OwnerUpdateRequest) : Observable<void>
  {
    return this.http.put<void>(`${environment.apiBaseUrl}/api/v2/owners/${id}`,ownerWithUpdates);
  }

}
