import { Injectable } from '@angular/core';
import { CreateFlatRequest } from '../interfaces/flat-create-request';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class FlatService {
 
  constructor(private http : HttpClient) { }

  createFlat(flatToCreate: CreateFlatRequest): Observable<string> {
    return this.http.post<string>(`${environment.apiBaseUrl}/api/v2/flats`, flatToCreate);
  }
}
