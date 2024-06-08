import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Owner } from '../interfaces/owner';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class OwnerService {

  constructor(private http: HttpClient) { }

  getOwnerById(ownerId : string) : Observable<Owner>
  {
    return this.http.get<Owner>(`${environment.apiBaseUrl}/api/v2/owners/${ownerId}`);
  }
}
