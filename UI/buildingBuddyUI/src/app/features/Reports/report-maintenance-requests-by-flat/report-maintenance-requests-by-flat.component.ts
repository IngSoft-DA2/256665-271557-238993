import { Component, OnInit } from '@angular/core';
import { NodeReportMaintenanceRequestsByBuilding } from '../interfaces/node-report-maintenance-requests-by-building';
import { ReportService } from '../services/report.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingService } from '../../Building/Services/building.service';
import { Building } from '../../Building/Interfaces/Building.model';
import { ManagerService } from '../../manager/services/manager.service';
import { NodeReportMaintenanceRequestsByFlat } from '../interfaces/node-report-maintenance-req-by-flat';
import { LoginService } from '../../login/services/login.service';
import { User } from '../../login/interfaces/user';
import { SystemUserRoleEnum } from '../../invitation/interfaces/enums/system-user-role-enum';

@Component({
  selector: 'app-report-maintenance-requests-by-building',
  templateUrl: './report-maintenance-requests-by-flat.component.html',
  styleUrls: ['./report-maintenance-requests-by-flat.component.css']
})
export class ReportMaintenanceRequestsByFlatComponent implements OnInit {
  reportOfMaintenanceRequestsByFlat?: NodeReportMaintenanceRequestsByFlat[];
  buildingIdSelected: string = "default";
  buildings: Building[] = [];
  buildingsIdList: string[] = [];
  managerId: string = "";
  userConnected?: User = undefined;
  SystemUserRoleEnumValues = SystemUserRoleEnum;


  constructor(
    private reportService: ReportService,
    private managerService: ManagerService,
    private buildingService: BuildingService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if (params['buildingId']) {
        this.buildingIdSelected = params['buildingId'];
      }
      if(params['managerId']){
        this.managerId = params['managerId'];
      }
      this.loadBuildings();
    });
  }

  loadReport(): void {
    if (this.buildingIdSelected && this.buildingIdSelected !== "default") {
      this.reportService.getReportMaintenanceRequestsByFlat(this.buildingIdSelected)
        .subscribe({
          next: (response) => {
            this.reportOfMaintenanceRequestsByFlat = response;
            console.log(this.reportOfMaintenanceRequestsByFlat);
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
                this.loadReport();
              },
              error: (error) => {
                console.error("Error al cargar el edificio:", error);
              }
            });
          });
          console.log(this.buildings);
          this.loadReport();
        },
        error: (error) => {
          console.error("Error al cargar los edificios:", error);
        }
      });
  }

  getBuildingName(buildingId: string): string {
    const buildingFound = this.buildings.find(b => b.id === buildingId);
    if (buildingFound) {
      return buildingFound.name;
    }
    
    return "";
  }

  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    this.buildingIdSelected = target.value;
    this.loadReport();
  }
}
