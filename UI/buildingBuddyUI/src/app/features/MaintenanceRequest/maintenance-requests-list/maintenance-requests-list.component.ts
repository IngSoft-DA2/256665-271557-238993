import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaintenanceRequest } from '../Interfaces/maintenanceRequest.model';
import { MaintenanceRequestService } from '../Services/maintenance-request.service';
import { LoginService } from '../../login/services/login.service';
import { User } from '../../login/interfaces/user';
import { SystemUserRoleEnum } from '../../invitation/interfaces/enums/system-user-role-enum';
import { HttpParams } from '@angular/common/http';
import { MaintenanceStatusEnum } from '../Interfaces/enums/maintenance-status-enum';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/interfaces/category';

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
  categoryId: string = "default";
  categories: Category[] = [];

  constructor(private router: Router, private maintenanceRequest: MaintenanceRequestService, private loginService: LoginService, private categoryService: CategoryService) { 
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
    this.loadCategories();
  }

  loadMaintenanceRequests(): void {
    this.maintenanceRequest.getAllMaintenanceRequests(this.managerId, this.categoryId)
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

  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
      this.categoryId = target.value;
      this.loadMaintenanceRequests();
  }

  loadCategories(): void {
    this.categoryService.getAllCategories()
      .subscribe({
        next: (response) => {
          this.categories = response;
          console.log(this.categories);
        },
        error: (error) => {
          console.error("Error al cargar los request handlers:", error);
        }
      });
  }

  getCategoryName(categoryId: string): string {
    const categoryFound = this.categories.find(r => r.id === categoryId);
    if (categoryFound) {
      return categoryFound.name;
    }
    return "";
  }
  
}
