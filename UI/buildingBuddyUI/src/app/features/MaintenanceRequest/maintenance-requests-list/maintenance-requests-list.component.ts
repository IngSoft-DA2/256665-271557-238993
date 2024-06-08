import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaintenanceRequest } from '../Interfaces/maintenanceRequest.model';
import { MaintenanceRequestService } from '../Services/maintenance-request.service';
import { LoginService } from '../../login/services/login.service';
import { User } from '../../login/interfaces/user';
import { SystemUserRoleEnum } from '../../invitation/interfaces/enums/system-user-role-enum';
import { HttpParams } from '@angular/common/http';
import { MaintenanceStatusEnum } from '../Interfaces/enums/maintenance-status-enum';

@Component({
  selector: 'app-maintenance-requests-list',
  templateUrl: './maintenance-requests-list.component.html',
  styleUrl: './maintenance-requests-list.component.css'
})
export class MaintenanceRequestsListComponent implements OnInit{

  maintenanceRequests: MaintenanceRequest[] = [];
  userConnected?: User = undefined;
  SystemUserRoleEnumValues = SystemUserRoleEnum;
  managerId: string = "";
  maintenanceRequestId: string = "";

  constructor(private router: Router, private maintenanceRequest: MaintenanceRequestService, private loginService: LoginService) { 
    loginService.getUser().subscribe({
      next: (response) => {
        this.userConnected = response;
        if (this.userConnected) {
          this.managerId = this.userConnected.userId;
        }
        console.log("Usuario encontrado, valores: " + this.userConnected);
      },
      error: () => {
        this.userConnected = undefined;
      }
    });
  }

  ngOnInit(): void {
    this.loadMaintenanceRequests();
  }

  loadMaintenanceRequests(): void {
    this.maintenanceRequest.getAllMaintenanceRequests(this.managerId)
      .subscribe({
        next: (response) => {
          this.maintenanceRequests = response;
          console.log(this.maintenanceRequests);
        },
        error: (error) => {
          console.error("Error al cargar las solicitudes de mantenimiento:", error);
        }
      });
  }

  getMaintenanceRequestClosedDate(maintenanceRequest: MaintenanceRequest): string {
    if(maintenanceRequest.closedDate){
      return maintenanceRequest.closedDate.toString();
    } 
    return "Not closed  yet";
  }

  getRequestHandlerName(maintenanceRequest: MaintenanceRequest): string {
    if(maintenanceRequest.requestHandler){
      return maintenanceRequest.requestHandler.name;
    }
    return "Not assigned yet";
  }

  getStatusString(status: number): string {
    switch (status) {
      case MaintenanceStatusEnum.Opened:
        return 'Opened';
      case MaintenanceStatusEnum.Closed:
        return 'Closed';
      case MaintenanceStatusEnum.OnAttendance:
        return 'On Attendance';
      default:
        return 'Unknown';
    }
  }

  goToMaintenanceRequestCreationForm(): void {
    const queryParams = new HttpParams()
    .set('managerId', this.managerId);

    this.router.navigateByUrl(`maintenance-requests/create?${queryParams}`)
  }

  goToMaintenanceRequestUpdateForm(maintenanceToUpdate: string){
    const queryParams = new HttpParams()
    .set('maintenanceRequestId', maintenanceToUpdate);

    this.router.navigateByUrl(`maintenance-requests/assign?${queryParams}`);

  }
}
