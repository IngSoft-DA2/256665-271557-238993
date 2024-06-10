import { Component, OnInit } from '@angular/core';
import { NodeReportMaintenanceRequestsByBuilding } from '../interfaces/node-report-maintenance-requests-by-building';
import { ReportService } from '../services/report.service';
import { ManagerService } from '../../manager/services/manager.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NodeReportMaintenanceRequestsByRequestHandler } from '../interfaces/node-report-maintenance-req-by-req-handler';
import { RequestHandler } from '../../requestHandler/interfaces/RequestHandler.model';
import { RequestHandlerService } from '../../requestHandler/services/request-handler.service';
import { BuildingService } from '../../building/services/building.service';
import { Building } from '../../building/interfaces/building';

@Component({
  selector: 'app-report-maintenance-req-by-req-handler',
  templateUrl: './report-maintenance-req-by-req-handler.component.html',
  styleUrls: ['./report-maintenance-req-by-req-handler.component.css']
})
export class ReportMaintenanceReqByReqHandlerComponent implements OnInit {
  reportOfMaintenanceRequestsByRequestHandler?: NodeReportMaintenanceRequestsByRequestHandler[];
  buildingIdSelected: string = "default";
  buildings: Building[] = [];
  buildingsIdList: string[] = [];
  managerId: string = "";
  requestHandlerId: string = "default";
  requestHandlers: RequestHandler[] = [];

  constructor(
    private reportService: ReportService,
    private managerService: ManagerService,
    private buildingService: BuildingService,
    private requestHandlerService: RequestHandlerService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.managerId = params['managerId'];
      if (params['buildingId']) {
        this.buildingIdSelected = params['buildingId'];
      }
      if (params['requestHandlerId']) {
        this.requestHandlerId = params['requestHandlerId'];
      }
      this.loadBuildings();
      this.loadRequestHandlers();
    });
  }

  loadReport(): void {
    if (this.buildingIdSelected !== "default" && this.requestHandlerId !== "default") {
      this.reportService.getReportMaintenanceRequestsByRequestHandler(this.managerId, this.buildingIdSelected, this.requestHandlerId)
        .subscribe({
          next: (response) => {
            this.reportOfMaintenanceRequestsByRequestHandler = response;
            console.log(this.reportOfMaintenanceRequestsByRequestHandler);
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
          this.buildingsIdList = response.buildingsId;
          this.buildingsIdList.forEach(id => {
            this.buildingService.getBuildingById(id).subscribe({
              next: (building) => {
                this.buildings.push(building);
                if (this.buildingIdSelected === "default" && this.buildings.length > 0) {
                  this.buildingIdSelected = this.buildings[0].id;
                }
                this.loadReport();
              },
              error: (error) => {
                console.error("Error al cargar el edificio:", error);
              }
            });
          });
        },
        error: (error) => {
          console.error("Error al cargar los edificios:", error);
        }
      });
  }

  loadRequestHandlers(): void {
    this.requestHandlerService.getAllRequestHandlers()
      .subscribe({
        next: (response) => {
          this.requestHandlers = response;
          console.log(this.requestHandlers);
        },
        error: (error) => {
          console.error("Error al cargar los request handlers:", error);
        }
      });
  }

  getRequestHandlerName(requestHandlerId: string): string {
    const requestHandlerFound = this.requestHandlers.find(r => r.id === requestHandlerId);
    if (requestHandlerFound) {
      return requestHandlerFound.name;
    }
    return "No request handler assigned yet";
  }

  onChange(event: Event, type: 'building' | 'requestHandler') {
    console.log(event);
    const target = event.target as HTMLSelectElement;
    if (type === 'building') {
      this.buildingIdSelected = target.value;
    } else if (type === 'requestHandler') {
      this.requestHandlerId = target.value;
      this.loadReport();
    }
    else{
      this.buildingIdSelected = "default";
      this.requestHandlerId = "default";
      this.loadReport();
    }
    this.loadReport();
  }

  formatTimeSpan(timeSpanString: string): string {
    if(timeSpanString === '00:00:00') {
        return 'There is no time to show';
    }
    // Dividir la cadena del TimeSpan en partes
    const parts = timeSpanString.split(':');

    // Extraer los días (si existen)
    let days = 0;
    if (parts[0].includes('.')) {
        const daysPart = parts[0].split('.')[0];
        days = parseInt(daysPart, 10);
        parts[0] = parts[0].split('.')[1];
    }

    // Convertir las partes a números
    const hours = parseInt(parts[0], 10);
    const minutes = parseInt(parts[1], 10);
    const secondsWithMilliseconds = parts[2].split('.');
    const seconds = parseInt(secondsWithMilliseconds[0], 10);
    const milliseconds = parseInt(secondsWithMilliseconds[1] || '0', 10);

    // Formatear el tiempo en un formato legible
    const formattedTime = `${days ? days + 'd ' : ''}${hours}h ${minutes}m`;

    return formattedTime;
}


}
