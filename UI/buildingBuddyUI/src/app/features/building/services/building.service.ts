import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Building } from '../interfaces/building';
import { environment } from '../../../../environments/environment.development';
import { BuildingUpdateRequest } from '../interfaces/building-update-request';
import { CreateBuildingRequest } from '../interfaces/building-create-request';
import { CreateBuildingResponse } from '../interfaces/building-create-response';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  constructor(private http : HttpClient) { }

  getAllBuildings() : Observable<Building[]>
  {
    return this.http.get<Building[]>(`${environment.apiBaseUrl}/api/v2/buildings`)
  }

  getBuildingById(buildingId: string) : Observable<Building>
  {
    return this.http.get<Building>(`${environment.apiBaseUrl}/api/v2/buildings/${buildingId}`)
  }

  deleteBuilding(buildingId: string) : Observable<void>
  {
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/v2/buildings/${buildingId}`)
  }

  updateBuilding(buildingId: string, buildingToUpdate: BuildingUpdateRequest) : Observable<void>
  {
    return this.http.put<void>(`${environment.apiBaseUrl}/api/v2/buildings/${buildingId}`,buildingToUpdate);
  }

  createBuilding(buildingToCreate: CreateBuildingRequest) : Observable<CreateBuildingResponse>
  {
    return this.http.post<CreateBuildingResponse>(`${environment.apiBaseUrl}/api/v2/buildings/`,buildingToCreate);
  }

}
