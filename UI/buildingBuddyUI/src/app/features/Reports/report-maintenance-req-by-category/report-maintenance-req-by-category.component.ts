import { Component } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { AdminService } from "../../administrator/services/admin.service";
import { Building } from "../../building/interfaces/building";
import { BuildingService } from "../../building/services/building.service";
import { Category } from "../../category/interfaces/category";
import { CategoryService } from "../../category/services/category.service";
import { NodeReportMaintenanceRequestsByCategory } from "../interfaces/node-report-maintenance-request-by-category";
import { ReportService } from "../services/report.service";


@Component({
  selector: 'app-report-maintenance-req-by-category',
  templateUrl: './report-maintenance-req-by-category.component.html',
  styleUrl: './report-maintenance-req-by-category.component.css'
})
export class ReportMaintenanceReqByCategoryComponent {
  reportOfMaintenanceRequestsByCategory?: NodeReportMaintenanceRequestsByCategory[];
  buildingIdSelected: string = "default";
  buildings: Building[] = [];
  buildingsIdList: string[] = [];
  managerId: string = "";
  categoryId: string = "default";
  categories: Category[] = [];

  emptyGuid: string = '00000000-0000-0000-0000-000000000000';

  constructor(
    private reportService: ReportService,
    private adminService: AdminService,
    private buildingService: BuildingService,
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if (params['buildingId']) {
        this.buildingIdSelected = params['buildingId'];
      }
      if (params['categoryId']) {
        this.categoryId = params['categoryId'];
      }
      this.loadBuildings();
      this.loadCategories();
    });
  }

  loadReport(): void {
    if (this.buildingIdSelected !== "default" && this.categoryId !== "default") {
      this.reportService.getReportMaintenanceRequestsByCategory(this.buildingIdSelected, this.categoryId)
        .subscribe({
          next: (response) => {
            this.reportOfMaintenanceRequestsByCategory = response;
            console.log(this.reportOfMaintenanceRequestsByCategory);
          },
          error: (error) => {
            console.error("Error al cargar el reporte:", error);
          }
        });
    }
  }

  loadBuildings(): void {
    this.buildingService.getAllBuildings(this.emptyGuid)
      .subscribe({
        next: (response) => {
          this.buildings = response;
                this.loadReport();
              }, 
              error: (error) => {
                console.error("Error al cargar los edificios:", error);
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

  getCategoryName(categoryId: string): string {
    const categoryFound = this.categories.find(r => r.id === categoryId);
    if (categoryFound) {
      return categoryFound.name;
    }
    return "";
  }

  onChange(event: Event, type: 'building' | 'category') {
    const target = event.target as HTMLSelectElement;
    if (type === 'building') {
      this.buildingIdSelected = target.value;
    } else if (type === 'category') {
      this.categoryId = target.value;
    }
    this.loadReport();
  }


}
