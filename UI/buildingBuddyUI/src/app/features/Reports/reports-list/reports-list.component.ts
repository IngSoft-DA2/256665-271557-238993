import { HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-reports-list',
  templateUrl: './reports-list.component.html',
  styleUrl: './reports-list.component.css'
})
export class ReportsListComponent {
  managerId: string = "e7503a12-821a-45f3-93f3-525ed1a79efd";
  buildingId: string = "00000000-0000-0000-0000-000000000000";
  requestHandlerId : string = "00000000-0000-0000-0000-000000000000";
  categoryId : string = "00000000-0000-0000-0000-000000000000";

  constructor(private router: Router, private route: ActivatedRoute){}

  goToReportMaintenanceRequestByBuilding(){
    const queryParams = new HttpParams()
    .set('managerId', this.managerId)
    .set('buildingId', this.buildingId);
    this.router.navigateByUrl(`reports/requests-by-building?${queryParams}`)
  }

  goToReportMaintenanceRequestByRequestHandler(){
    const queryParams = new HttpParams()
    .set('requestHandlerId', this.requestHandlerId)
    .set('buildingId', this.buildingId)
    .set('managerId', this.managerId);
    this.router.navigateByUrl(`reports/requests-by-request-handler?${queryParams}`)
  }

  goToReportMaintenanceRequestByCategory(){
    const queryParams = new HttpParams()
    .set('buildingId', this.buildingId)
    .set('categoryId', this.categoryId);
    this.router.navigateByUrl(`reports/requests-by-category?${queryParams}`)
  }

  goToReportMaintenanceRequestByFlat(){
    const queryParams = new HttpParams()
    .set('buildingId', this.buildingId);
    this.router.navigateByUrl(`reports/requests-by-flat?${queryParams}`)
  }


}
