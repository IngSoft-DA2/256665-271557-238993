import { Component } from '@angular/core';
import { NodeReportMaintenanceRequestsByBuilding } from './interfaces/node-report-maintenance-requests-by-building';
import { ReportService } from '../services/report.service';
import { Router } from '@angular/router';
import { BuildingService } from '../../Building/Services/building.service';
import { Building } from '../../Building/Interfaces/Building.model';

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
  userId?: string

  constructor(private reportMaintenanceRequestByBuildingService: ReportService, private buildingService: BuildingService, private router: Router){
    this.getMaintenanceRequestsByBuilding(this.buildingIdSelected);
    this.buildings = this.buildingService.getAllBuildings(this.userId);
  }

  getMaintenanceRequestsByBuilding(buildingIdSelected?: string){
    this.reportOfMaintenanceRequestsByBuilding = this.reportMaintenanceRequestByBuildingService.getReportMaintenanceRequestsByBuilding(buildingIdSelected);
  }

  getBuildingInfo(buildingId: string){
    this.buildingName = this.buildingService.getBuildingById(buildingId).name;
  }
}