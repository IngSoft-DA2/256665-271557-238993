import { Component } from '@angular/core';
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
export class ReportMaintenanceRequestsByBuildingComponent {
  reportOfMaintenanceRequestsByBuilding?: NodeReportMaintenanceRequestsByBuilding[];
  buildingName?: string;
  buildingIdSelected?: string;
  buildings?: Building[];
  userId?: string;

  constructor(private reportMaintenanceRequestByBuildingService: ReportService, private buildingService: BuildingService, private router: Router, private route: ActivatedRoute){
    this.getMaintenanceRequestsByBuilding(this.buildingIdSelected);
    this.buildingService.getAllBuildings(this.userId);
  }

  getMaintenanceRequestsByBuildings(userId:string, buildingIdSelected: string){
      this.reportMaintenanceRequestByBuildingService.getReportMaintenanceRequestsByBuilding(userId, buildingIdSelected);
  }

  getBuildingInfo(buildingId: string){
    this.buildingService.getBuildingById(buildingId).
    subscribe({
      next: (response) => {
        this.buildingName = response.name;
      },
      error: (errorMessage) => {
        alert(errorMessage.error);
      }
    });
  }


}