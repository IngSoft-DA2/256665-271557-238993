import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { FlatCreateRequest } from '../interfaces/flat-create-request';


@Injectable({
  providedIn: 'root'
})
export class FlatService {
 
  constructor(private http : HttpClient) { }

  createFlat(flatToCreate: FlatCreateRequest): Observable<string> {
    return this.http.post<string>(`${environment.apiBaseUrl}/api/v2/flats?addAuth=true`, flatToCreate);
  }
}
