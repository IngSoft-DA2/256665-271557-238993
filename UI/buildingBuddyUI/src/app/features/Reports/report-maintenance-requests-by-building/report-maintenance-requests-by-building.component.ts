import { Component, OnInit } from '@angular/core';
import { NodeReportMaintenanceRequestsByBuilding } from './interfaces/node-report-maintenance-requests-by-building';
import { ReportService } from '../services/report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingService } from '../../Building/Services/building.service';
import { Building } from '../../Building/Interfaces/Building.model';
import { ManagerService } from '../../manager/services/manager.service';

@Component({
  selector: 'app-report-maintenance-requests-by-building',
  templateUrl: './report-maintenance-requests-by-building.component.html',
  styleUrls: ['./report-maintenance-requests-by-building.component.css']
})
export class ReportMaintenanceRequestsByBuildingComponent implements OnInit {
  reportOfMaintenanceRequestsByBuilding?: NodeReportMaintenanceRequestsByBuilding[];
  buildingIdSelected: string = "default";
  buildings: Building[] = [];
  buildingsIdList: string[] = [];
  managerId: string = "";

  constructor(
    private reportService: ReportService,
    private managerService: ManagerService,
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
    });
  }

  loadReport(): void {
    if (this.buildingIdSelected && this.buildingIdSelected !== "default") {
      this.reportService.getReportMaintenanceRequestsByBuilding(this.managerId, this.buildingIdSelected)
        .subscribe({
          next: (response) => {
            this.reportOfMaintenanceRequestsByBuilding = response;
            console.log(this.reportOfMaintenanceRequestsByBuilding);
          },
          error: (error) => {
            console.error("Error al cargar el reporte:", error);
          }
        });
    }
  }

  loadBuildings(): void {
    this.managerService.getManagerById(this.managerId)
      .subscribe({
        next: (response) => {
          this.buildingsIdList = response.buildings;
          if (this.buildingIdSelected === "default" && this.buildings.length > 0) {
            this.buildingIdSelected = "default";
          }
          this.buildingsIdList.forEach(id => {
            this.buildingService.getBuildingById(id).subscribe({
              next: (building) => {
                this.buildings.push(building);
                if (this.buildingIdSelected === "default" && this.buildings.length > 0) {
                  this.buildingIdSelected = this.buildings[0].id;
                }
                console.log("Edificios cargados: ");
                console.log(this.buildingsIdList);
                this.loadReport();
              },
              error: (error) => {
                console.error("Error al cargar el edificio:", error);
              }
            });
          });
          console.log(this.buildings);
          this.loadReport();
          console.log(this.reportOfMaintenanceRequestsByBuilding);
        },
        error: (error) => {
          console.error("Error al cargar los edificios:", error);
        }
      });
  }

  getBuildingName(buildingId: string): string {
    console.log("buildingId:  aa");
    console.log(buildingId);
    console.log("Buildings cargados en getBuildingName: ");
    console.log(this.buildings);
    const buildingFound = this.buildings.find(b => b.id === buildingId);
    console.log(buildingFound);
    if (buildingFound) {
      return buildingFound.name;
    }
    
    return "pedo";

  }

  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    this.buildingIdSelected = target.value;
    this.loadReport();
  }
}
