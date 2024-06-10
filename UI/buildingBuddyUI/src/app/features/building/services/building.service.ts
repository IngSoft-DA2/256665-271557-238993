import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Building } from '../interfaces/building';
import { environment } from '../../../../environments/environment.development';
import { BuildingUpdateRequest } from '../interfaces/building-update-request';
import { CreateBuildingResponse } from '../interfaces/building-create-response';
import { BuildingCreateRequest } from '../interfaces/building-create-request';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  constructor(private http : HttpClient) { }

  getAllBuildings(userId : string) : Observable<Building[]>
  {
    const params = new HttpParams().set('userId', userId);
    return this.http.get<Building[]>(`${environment.apiBaseUrl}/api/v2/buildings`,{params});
  }

  getBuildingById(buildingId: string) : Observable<Building>
  {
    return this.http.get<Building>(`${environment.apiBaseUrl}/api/v2/buildings/${buildingId}`);
  }

  deleteBuilding(buildingId: string) : Observable<void>
  {
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/v2/buildings/${buildingId}`);
  }

  updateBuilding(buildingId: string, buildingToUpdate: BuildingUpdateRequest) : Observable<void>
  {
    return this.http.put<void>(`${environment.apiBaseUrl}/api/v2/buildings/${buildingId}`,buildingToUpdate);
  }

  createBuilding(buildingToCreate: BuildingCreateRequest) : Observable<CreateBuildingResponse>
  {
    return this.http.post<CreateBuildingResponse>(`${environment.apiBaseUrl}/api/v2/buildings/`,buildingToCreate);
  }

  loadBuildings(file: File) : Observable<string>
  {
    const formData = new FormData();
    formData.append('file',file);
    return this.http.post<string>(`${environment.apiBaseUrl}/api/v2/buildings/upload`,formData);
  }

}
