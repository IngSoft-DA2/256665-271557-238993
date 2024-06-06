import { Component, OnInit } from '@angular/core';
import { NodeReportMaintenanceRequestsByBuilding } from './interfaces/node-report-maintenance-requests-by-building';
import { ReportService } from '../services/report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingService } from '../../Building/Services/building.service';
import { Building } from '../../Building/Interfaces/Building.model';

@Component({
  selector: 'app-report-maintenance-requests-by-building',
  templateUrl: './report-maintenance-requests-by-building.component.html',
  styleUrls: ['./report-maintenance-requests-by-building.component.css']
})
export class ReportMaintenanceRequestsByBuildingComponent implements OnInit {
  reportOfMaintenanceRequestsByBuilding?: NodeReportMaintenanceRequestsByBuilding[];
  buildingIdSelected: string = "default";
  buildings: Building[] = [];
  managerId: string = "";

  constructor(
    private reportService: ReportService,
    private buildingService: BuildingService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.managerId = params['managerId'];
      if (params['buildingId']) {
        this.buildingIdSelected = params['buildingId'];
      }
      this.loadBuildings();
      console.log(this.buildings);
    });
  }

  loadReport(): void {
    if (this.buildingIdSelected && this.buildingIdSelected !== "default") {
      this.reportService.getReportMaintenanceRequestsByBuilding(this.managerId, this.buildingIdSelected)
        .subscribe({
          next: (response) => {
            this.reportOfMaintenanceRequestsByBuilding = response;
          },
          error: (error) => {
            console.error("Error al cargar el reporte:", error);
          }
        });
    }
  }

  loadBuildings(): void {
    this.buildingService.getAllBuildings(this.managerId)
      .subscribe({
        next: (response) => {
          this.buildings = response;
          if (this.buildingIdSelected === "default" && this.buildings.length > 0) {
            this.buildingIdSelected = "default";
          }
          this.loadReport();
        },
        error: (error) => {
          console.error("Error al cargar los edificios:", error);
        }
      });
  }

  getBuildingName(buildingId: string): string {
    const building = this.buildings.find(b => b.id === buildingId);
    if(building){
    return building.name;
    }
    else{
      return "";
    }
  }

  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    this.buildingIdSelected = target.value;
    this.loadReport();
  }
}
