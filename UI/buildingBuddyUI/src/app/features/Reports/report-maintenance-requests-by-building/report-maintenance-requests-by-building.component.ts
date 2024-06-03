import { Component } from '@angular/core';
import { NodeReportMaintenanceRequestsByBuilding } from './interfaces/node-report-maintenance-requests-by-building';
import { ReportService } from '../services/report.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-report-maintenance-requests-by-building',
  templateUrl: './report-maintenance-requests-by-building.component.html',
  styleUrl: './report-maintenance-requests-by-building.component.css'
})
export class ReportMaintenanceRequestsByBuildingComponent {
  reportOfMaintenanceRequestsByBuilding?: NodeReportMaintenanceRequestsByBuilding[];
  buildingName?: string;

  constructor(private reportMaintenanceRequestByBuildingService: ReportService, , private buildingService: BuildingService, private router: Router){}

  getBuildingInfo(buildingId: string){
    this.

  }
}