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
export class BuildingUpdateComponent implements OnInit {
  buildingToUpdate?: Building;
  buildingWithUpdates: BuildingUpdateRequest = {
    managerId: '',
    commonExpenses: 0
  };

  buildingId: string | undefined = undefined;
  availableManagers: Manager[] = [];
  availableManagerMessage: string = '';
  areAvailableManagers: boolean = false;

  constructor(
    private buildingService: BuildingService,
    private managerService: ManagerService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.obtainIdFromRoute();
    this.loadManagers();
    if (this.buildingId) {
      this.loadBuilding();
    } else {
      this.redirectToBuildingList();
    }
  }

  private obtainIdFromRoute(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.buildingId = params.get('buildingId') ?? undefined;
      }
    });
  }

  private loadManagers(): void {
    this.managerService.getAllManagers().subscribe({
      next: (response) => {
        this.availableManagers = response;
        this.updateAvailableManagers();
      },
      error: (errorMessage) => {
        alert(errorMessage.error);
      }
    });
  }

  private loadBuilding(): void {
    this.buildingService.getBuildingById(this.buildingId!).subscribe({
      next: (response) => {
        this.buildingToUpdate = response;
        this.updateAvailableManagers();
      },
      error: () => {
        alert('This building cannot be found.');
        this.redirectToBuildingList();
      }
    });
  }

  private updateAvailableManagers(): void {
    if (this.buildingToUpdate) {
      this.availableManagers = this.availableManagers.filter(m => m.id !== this.buildingToUpdate!.manager.id);
      this.areAvailableManagers = this.availableManagers.length > 0;
      this.availableManagerMessage = this.areAvailableManagers
        ? 'There are available managers to swap with!, press click in the select box.'
        : 'There are no available managers at the moment to swap with.';
    }
  }

  private redirectToBuildingList(): void {
    alert('Building was not found, redirecting to building list...');
    this.router.navigateByUrl('/buildings/list');
  }

  updateBuilding(): void {
    if (this.buildingToUpdate && this.buildingId) {
      this.buildingWithUpdates.commonExpenses = this.buildingToUpdate.commonExpenses;

      if(this.buildingWithUpdates.managerId === ''){
       this.buildingWithUpdates.managerId = this.buildingToUpdate.manager.id;
      }
      this.buildingService.updateBuilding(this.buildingId, this.buildingWithUpdates).subscribe({
        next: () => {
          alert('Building updated successfully');
          this.router.navigateByUrl('/buildings/list');
        },
        error: (errorMessage) => {
          alert(errorMessage.error);
        }
      });
    } else {
      alert('A problem was found. Try again please.');
      this.redirectToBuildingList();
    }
  }

  onChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const value = target?.value || '';
    this.buildingWithUpdates.managerId = value;
  }
}
