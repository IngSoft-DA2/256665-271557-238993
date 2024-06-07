import { HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../login/interfaces/user';
import { SystemUserRoleEnum } from '../../invitation/interfaces/enums/system-user-role-enum';
import { LoginService } from '../../login/services/login.service';

@Component({
  selector: 'app-reports-list',
  templateUrl: './reports-list.component.html',
  styleUrl: './reports-list.component.css'
})
export class ReportsListComponent {
  managerId: string = "";
  buildingId: string = "00000000-0000-0000-0000-000000000000";
  requestHandlerId : string = "00000000-0000-0000-0000-000000000000";
  categoryId : string = "00000000-0000-0000-0000-000000000000";
  userConnected?: User = undefined;
  SystemUserRoleEnumValues = SystemUserRoleEnum;

  constructor(private router: Router, private route: ActivatedRoute, private loginService: LoginService){
    loginService.getUser().subscribe({
      next: (Response) => {
        this.userConnected = Response
        if(this.userConnected){
          this.managerId = this.userConnected.userId;
        }
        console.log("Usuario encontrado, valores: " + this.userConnected)
      },
      error: () => {
        this.userConnected = undefined;
      }
    })
  }

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
