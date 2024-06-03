import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Building } from '../Interfaces/Building.model';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  constructor(private http: HttpClient) { }

  getBuildingById(buildingId: string) : Building{
    return this.http.get<Building>(`${environment.apiBaseUrl}/buildings`);
  }

  getAllBuildings(userId?: string) : Building[]{
    return this.http.get<Building[]>(`${environment.apiBaseUrl}/buildings/${userId}`);

  }
}
