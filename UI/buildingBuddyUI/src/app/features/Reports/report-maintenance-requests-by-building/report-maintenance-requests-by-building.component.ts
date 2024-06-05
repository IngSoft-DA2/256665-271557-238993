import { Component, OnDestroy, OnInit } from '@angular/core';
import { NodeReportMaintenanceRequestsByBuilding } from './interfaces/node-report-maintenance-requests-by-building';
import { ReportService } from '../services/report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingService } from '../../Building/Services/building.service';
import { Building } from '../../Building/Interfaces/Building.model';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-report-maintenance-requests-by-building',
  templateUrl: './report-maintenance-requests-by-building.component.html',
  styleUrl: './report-maintenance-requests-by-building.component.css'
})
export class ReportMaintenanceRequestsByBuildingComponent{
  reportOfMaintenanceRequestsByBuilding?: NodeReportMaintenanceRequestsByBuilding[];
  buildingIdSelected: string = "00000000-0000-0000-0000-000000000000";
  buildings: Building[] = [];
  managerId: string = "";
  buildingName: string = "";

  constructor(private reportMaintenanceRequestByBuildingService: ReportService, private buildingService: BuildingService, private router: Router, private route: ActivatedRoute){
    this.route.queryParams.subscribe(params => {
      this.managerId = params['managerId'];
      this.buildingIdSelected = params['buildingId'];
    });

    alert(this.managerId);

    this.reportMaintenanceRequestByBuildingService.getReportMaintenanceRequestsByBuilding(this.managerId, this.buildingIdSelected).
    subscribe({
      next: (Response) => {
        this.reportOfMaintenanceRequestsByBuilding = Response;
      },
    });
    //this.buildingService.getAllBuildings(this.managerId);
  }


  getBuildingInfo(buildingId: string) : string {
    this.buildingService.getBuildingById(buildingId).
    subscribe({
      next: (Response) => {
        this.buildingName = Response.name;
      }
    });
    
    return this.buildingName;
  }


}