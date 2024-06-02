import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class InvitationService {


  constructor(private http : HttpClient) { }

  getAllInvitations() : Observable<Invitation[]>
  {
    return this.http.get<Invitation[]>(`${environment.apiBaseUrl}/api/v2/invitations`);
  }

  
}
