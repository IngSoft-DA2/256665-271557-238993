import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Building } from '../interfaces/building';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {


  constructor(private http : HttpClient) { }

  getAllBuildings() : Observable<Building[]>
  {
    return this.http.get<Building[]>(`${environment.apiBaseUrl}/api/v2/buildings`)
  }

  deleteBuilding(buildingId: string) : Observable<void>
  {
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/v2/buildings/${buildingId}`)
  }
}
