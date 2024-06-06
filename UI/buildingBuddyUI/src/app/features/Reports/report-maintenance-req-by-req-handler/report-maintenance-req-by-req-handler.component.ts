import { Component } from '@angular/core';
import { NodeReportMaintenanceRequestsByRequestHandler } from '../interfaces/node-report-maintenance-req-by-req-handler';

@Component({
  selector: 'app-report-maintenance-req-by-req-handler',
  templateUrl: './report-maintenance-req-by-req-handler.component.html',
  styleUrl: './report-maintenance-req-by-req-handler.component.css'
})
export class ReportMaintenanceReqByReqHandlerComponent {
  reportOfMaintenanceRequestsByReqHandler?: NodeReportMaintenanceRequestsByRequestHandler[];
  buildingIdSelected: string = "default";
  buildings: RequestHandler[] = [];
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


}
