import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { Building } from "../../building/interfaces/building";
import { BuildingService } from "../../building/services/building.service";
import { Category } from "../../category/interfaces/category";
import { CategoryService } from "../../category/services/category.service";
import { Flat } from "../../flat/interfaces/flat";
import { ManagerService } from "../../manager/services/manager.service";
import { MaintenanceCreateRequest } from "../Interfaces/maintenance-create-request";
import { MaintenanceRequestService } from "../Services/maintenance-request.service";


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
  showFlats: boolean = false;
  flats: Flat[] = [];
  maintenanceCreateRequest: MaintenanceCreateRequest = {} as MaintenanceCreateRequest;
  categories: Category[] = [];

  constructor(
    private maintenanceRequestService: MaintenanceRequestService,
    private managerService: ManagerService, 
    private categoryService: CategoryService,
    private buildingService: BuildingService, 
    private router: Router, 
    private route: ActivatedRoute
  ) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
        this.managerId = params['managerId'];
    });
    
    this.loadCategories();
    this.loadBuildings();
  }

  loadBuildings(): void {
    this.managerService.getManagerById(this.managerId).subscribe({
      next: (response) => {
        this.buildingsIdList = response.buildingsId;
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
          // Obtener solo las categorías principales
          const mainCategories = response.map(category => {
            return {
              id: category.id,
              name: category.name,
              // Puedes añadir más propiedades aquí si es necesario
            };
          });
  
          // Obtener todas las subcategorías y fusionarlas en una sola lista
          const subcategories = response.flatMap(category => category.subCategories !== undefined ? category.subCategories : []);
  
          // Fusionar las categorías principales y las subcategorías en una sola lista
          this.categories = [...mainCategories, ...subcategories];
  
          console.log(this.categories);
        },
        error: (error) => {
          console.error("Error al cargar los request handlers:", error);
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
    if(this.flatId === "" || this.category === "" || this.managerId === "") {
      alert("Please fill all the fields");
      return;
    }

    this.maintenanceCreateRequest.flatId = this.flatId;
    this.maintenanceCreateRequest.category = this.category;
    this.maintenanceCreateRequest.managerId = this.managerId;
  
    this.maintenanceRequestService.createMaintenanceRequest(this.maintenanceCreateRequest).subscribe({
      next: (response) => {
        alert("Maintenance request created successfully");
        this.router.navigate(['../list'], {relativeTo: this.route});
      },
      error: (error) => {
        console.error("Error on maintenance request creation:", error);
        alert("Error on maintenance request creation " + error.error);
      }
    });
  }

  goToReportsList(): void { 
    this.router.navigate(['../list'], {relativeTo: this.route});
  }
}
