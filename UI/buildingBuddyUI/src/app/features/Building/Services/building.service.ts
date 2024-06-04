import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Building } from '../Interfaces/Building.model';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  constructor(private http: HttpClient) { }

  getBuildingById(buildingId: string) : Observable<Building>{
    return this.http.get<Building>(`${environment.apiBaseUrl}/buildings`);
  }

  getAllBuildings(userId?: string) : Observable<Building[]>{
    return this.http.get<Building[]>(`${environment.apiBaseUrl}/buildings/${userId}`);
  }
}
