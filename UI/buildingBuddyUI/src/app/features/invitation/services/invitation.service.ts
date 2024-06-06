import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { Invitation } from '../interfaces/invitation';
import { invitationUpdateRequest } from '../interfaces/invitation-update';
import { invitationCreateRequest } from '../interfaces/invitation-create';

@Injectable({
  providedIn: 'root'
})
export class InvitationService {

  constructor(private http : HttpClient) { }

  getAllInvitations() : Observable<Invitation[]>
  {
    return this.http.get<Invitation[]>(`${environment.apiBaseUrl}/api/v2/invitations`);
  }

  getInvitationsByEmail(email : string) : Observable<Invitation[]>
  {
    const params = new HttpParams().set('email', email);
    return this.http.get<Invitation[]>(`${environment.apiBaseUrl}/api/v2/invitations/guest`, {params});
  }

  deleteInvitation(id: string) : Observable<void>
  {
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/v2/invitations?addAuth=true/${id}`)
  }

  getInvitationById(id:string) : Observable<Invitation>
  {
    return this.http.get<Invitation>(`${environment.apiBaseUrl}/api/v2/invitations/${id}`)
  }

  updateInvitation(id:string, request : invitationUpdateRequest) : Observable<void>
  {
    return this.http.put<void>(`${environment.apiBaseUrl}/api/v2/invitations/${id}`,request)
  }

  createInvitation(request : invitationCreateRequest) : Observable<string>
  {
    return this.http.post<string>(`${environment.apiBaseUrl}/api/v2/invitations?addAuth=true`, request)
  }
  
  

  
}
