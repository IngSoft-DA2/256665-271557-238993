import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingService } from '../../Building/Services/building.service';
import { ManagerService } from '../../manager/services/manager.service';
import { Building } from '../../Building/Interfaces/Building.model';
import { LoginService } from '../../login/services/login.service';
import { User } from '../../login/interfaces/user';
import { SystemUserRoleEnum } from '../../invitation/interfaces/enums/system-user-role-enum';
import { Flat } from '../../Building/Interfaces/Flat.model';
import { MaintenanceCreateRequest } from '../Interfaces/maintenance-create-request';
import { MaintenanceRequestService } from '../Services/maintenance-request.service';
import { Category } from '../../category/interfaces/category';
import { CategoryService } from '../../category/services/category.service';

@Component({
  selector: 'app-create-maintenance-request',
  templateUrl: './create-maintenance-request.component.html',
  styleUrls: ['./create-maintenance-request.component.css']
})
export class CreateMaintenanceRequestComponent implements OnInit {
  description: string = "";
  flatId: string = "";
  category: string = "";
  buildingIdSelected: string = "default";
  buildings: Building[] = [];
  buildingsIdList: string[] = [];
  managerId: string = "";
  userConnected?: User = undefined;
  SystemUserRoleEnumValues = SystemUserRoleEnum;
  showFlats: boolean = false;
  flats: Flat[] = [];
  maintenanceCreateRequest: MaintenanceCreateRequest = {} as MaintenanceCreateRequest;
  categories: Category[] = [];

  constructor(
    private loginService: LoginService, 
    private maintenanceRequestService: MaintenanceRequestService,
    private managerService: ManagerService, 
    private categoryService: CategoryService,
    private buildingService: BuildingService, 
    private router: Router, 
    private route: ActivatedRoute
  ) { 
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
    this.loadBuildings();
    this.loadCategories();
  }

  loadBuildings(): void {
    this.managerService.getManagerById(this.managerId).subscribe({
      next: (response) => {
        this.buildingsIdList = response.buildings;
        this.buildingsIdList.forEach(id => {
          this.buildingService.getBuildingById(id).subscribe({
            next: (building) => {
              this.buildings.push(building);
              if (this.buildingIdSelected === "default" && this.buildings.length > 0) {
                this.buildingIdSelected = this.buildings[0].id;
              }
              console.log("Edificios cargados: ", this.buildings);
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

  loadFlats(buildingId: string): void {
    this.buildingService.getBuildingById(buildingId).subscribe({
      next: (response) => {
        this.flats = response.flats;
        console.log("Flats cargados: ", this.flats);
      },
      error: (error) => {
        console.error("Error al cargar los flats:", error);
      }
    });
  }

  loadCategories(): void {
    this.categoryService.getAllCategories()
      .subscribe({
        next: (response) => {
          this.categories = response;
          console.log(this.categories);
        },
        error: (error) => {
          console.error("Error on category loading:", error);
        }
      });
  }

  onChange(event: Event, type: 'building' | 'flat' | 'category') {
    const target = event.target as HTMLSelectElement;
    if (type === 'building') {
      this.buildingIdSelected = target.value;
      this.showFlats = true;
      this.loadFlats(this.buildingIdSelected); 
    } else if (type === 'flat') {
      this.flatId = target.value;
    }
    else if (type === 'category') {
      this.category = target.value;
    }
  }

  createMaintenanceRequest(): void {
    this.maintenanceCreateRequest.description = this.description;
    this.maintenanceCreateRequest.flatId = this.flatId;
    this.maintenanceCreateRequest.category = this.category;
    this.maintenanceCreateRequest.managerId = this.managerId;

    alert(this.maintenanceCreateRequest.description);
    alert(this.maintenanceCreateRequest.flatId);
    alert(this.maintenanceCreateRequest.category);
    alert(this.maintenanceCreateRequest.managerId);
  

    this.maintenanceRequestService.createMaintenanceRequest(this.maintenanceCreateRequest).subscribe({
      next: (response) => {
        console.log("Solicitud de mantenimiento creada con éxito:", response);
        alert("Solicitud de mantenimiento creada con éxito");
        this.router.navigate(['../list'], {relativeTo: this.route});
      },
      error: (error) => {
        alert("Error on maintenance request creation");
        console.error("Error on creation: ", error);
      }
    });
  }
}
