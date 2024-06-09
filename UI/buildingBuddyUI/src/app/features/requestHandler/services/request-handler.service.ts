import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RequestHandler } from '../interfaces/RequestHandler.model';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RequestHandlerService {

  constructor(private http: HttpClient) { }

  getAllRequestHandlers() : Observable<RequestHandler[]> {
    return this.http.get<RequestHandler[]>(`${environment.apiBaseUrl}/api/v2/request-handlers`);  
  }

  
}
