
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingService } from '../services/building.service';
import { Building } from '../interfaces/building';
import { Manager } from '../../manager/interfaces/manager';
import { ManagerService } from '../../manager/services/manager.service';
import { BuildingUpdateRequest } from '../interfaces/building-update-request';

@Component({
  selector: 'app-building-update',
  templateUrl: './building-update.component.html',
  styleUrl: './building-update.component.css'
})
export class BuildingUpdateComponent {
  buildingToUpdate?: Building
  buildingId: string | undefined = undefined;
  managersAvailable: Manager[] = [];
  availableManagerMessage: string = '';
  areAvailableManagers: boolean = false;

  constructor(private buildingService: BuildingService, private managerService: ManagerService, private router: Router, private route: ActivatedRoute) {

    this.obtainIdFromRoute();
    this.obtainAvailableManagers();

    if (this.buildingId) {
      this.buildingService.getBuildingById(this.buildingId)
        .subscribe({
          next: (Response) => {
            this.buildingToUpdate = Response;
            console.log(this.buildingToUpdate);
            if (this.buildingToUpdate) {
              this.managersAvailable = this.managersAvailable.filter(m => m.id !== this.buildingToUpdate?.manager.id)
              if (this.managersAvailable.length > 0) {
                this.areAvailableManagers = true;
                this.availableManagerMessage = 'There are available managers to swap with!, press click in the select box.';
                console.log(this.managersAvailable);
              }
              else {
                this.areAvailableManagers = false;
                this.availableManagerMessage = 'There are no available managers at the moment to swap with.'
              }
            }
          },
          error: () => {
            alert("This building cannot be found.");
            this.router.navigateByUrl('/buildings/list');
          }
        });
    }
    else {
      alert("Building was not found, redirecting to building list...");
      this.router.navigateByUrl('/buildings/list');
    }
  }


  private obtainIdFromRoute(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.buildingId = params.get('buildingId') ?? undefined;
      }
    })
  }

  obtainAvailableManagers() {
    this.managerService.getAllManagers()
      .subscribe({
        next: (Response) => {
          this.managersAvailable = Response;
        },
        error: () => {
          this.managersAvailable = [];
        }
      })
  }

  updateBuilding(): void {

    if (this.buildingToUpdate && this.buildingId) {

      const buildingWithUpdates: BuildingUpdateRequest =
      {
        managerId: this.buildingToUpdate.manager.id,
        commonExpenses: this.buildingToUpdate.commonExpenses
      };

      this.buildingService.updateBuilding(this.buildingId, buildingWithUpdates)
        .subscribe({
          next: () => {
            alert("Building updated sucessfully");
            this.router.navigateByUrl('/buildings/list');
          },
          error: (errorMessage) => {
            alert(errorMessage.error);
          }
        });
    }
    else {
      alert("A problem was found. Try again please.");
      this.router.navigateByUrl('/buildings/list');
    }
  }
}

