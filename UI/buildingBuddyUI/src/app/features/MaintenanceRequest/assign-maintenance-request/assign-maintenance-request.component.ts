import { Component, OnInit, AfterContentInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Category } from "../../category/interfaces/category";
import { CategoryService } from "../../category/services/category.service";
import { RequestHandler } from "../../requestHandler/interfaces/RequestHandler.model";
import { RequestHandlerService } from "../../requestHandler/services/request-handler.service";
import { MaintenanceRequest } from "../Interfaces/maintenanceRequest.model";
import { MaintenanceRequestService } from "../Services/maintenance-request.service";


@Component({
  selector: 'app-assign-maintenance-request',
  templateUrl: './assign-maintenance-request.component.html',
  styleUrl: './assign-maintenance-request.component.css'
})
export class AssignMaintenanceRequestComponent implements OnInit, AfterContentInit{

  maintenanceRequestId: string = "";
  requestHandlerId: string = "default";
  requestHandlers : RequestHandler[] = [];
  maintenanceRequestToUpdate: MaintenanceRequest = {} as MaintenanceRequest;
  categoryOfMaintenanceRequest: Category = {} as Category;

  constructor(private requestHandlerService: RequestHandlerService, private maintenanceRequestService: MaintenanceRequestService, private categoryService: CategoryService, 
    private route: ActivatedRoute, private router: Router) {
    this.route.queryParams.subscribe(params => {
      if (params['maintenanceRequestId']) {
        this.maintenanceRequestId = params['maintenanceRequestId'];
      }
  });
  }

  ngOnInit(): void {
    this.loadRequestHandlers();
    this.loadMaintenanceRequestData();
    this.loadCategoryOfMaintenanceRequest();
  }

  ngAfterContentInit(): void {
    this.loadCategoryOfMaintenanceRequest();
  }

  loadMaintenanceRequestData(): void {
    this.maintenanceRequestService.getMaintenanceRequestById(this.maintenanceRequestId)
      .subscribe({
        next: (Response) => {
          this.maintenanceRequestToUpdate = Response;
          this.loadCategoryOfMaintenanceRequest();
        },
        error: (error) => {
          alert("Error on maintenance request loading: " + error);
        }
      });
  }

  loadCategoryOfMaintenanceRequest(): void {
      this.categoryService.getCategoryById(this.maintenanceRequestToUpdate.category)
      .subscribe({
        next: (Response) => {
          this.categoryOfMaintenanceRequest = Response;
        },
        error: (error) => {
          console.error("Error on category loading: ", error);
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
          console.error("Error on request handler loading: ", error);
        }
      });
  }

  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
      this.requestHandlerId = target.value;
  }

  assignMaintenanceRequest(): void {
    if(this.requestHandlerId === "default") {
      alert("Please select a request handler");
    }
    else{
    this.maintenanceRequestService.assignMaintenanceRequest(this.maintenanceRequestToUpdate.id, this.requestHandlerId)
      .subscribe({
        next: (response) => {
          alert(" Maintenance request assigned successfully");
          this.goToMaintenanceRequestList();
        },
        error: (error) => {
          alert("Error on assign, please check the request");
        }
      });
    }
  }

  goToMaintenanceRequestList(): void {
    this.router.navigate(['../list'], {relativeTo: this.route});
  }

}
