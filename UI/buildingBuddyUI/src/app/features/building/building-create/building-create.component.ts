import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingService } from '../services/building.service';
import { FlatService } from '../../flat/services/flat.service';
import { OwnerService } from '../../owner/services/owner.service';
import { Owner } from '../../owner/interfaces/owner';
import { FlatCreateRequest } from '../../flat/interfaces/flat-create-request';
import { BuildingCreateRequest } from '../interfaces/building-create-request';
import { Manager } from '../../manager/interfaces/manager';
import { ManagerService } from '../../manager/services/manager.service';

@Component({
  selector: 'app-building-create',
  templateUrl: './building-create.component.html',
  styleUrl: './building-create.component.css'
})
export class BuildingCreateComponent {

  showOwnerCreation: boolean = false;
  ownerId: string = 'default';

  buildingToCreate: BuildingCreateRequest = {
    managerId: '',
    constructionCompanyId: '',
    name: '',
    address: '',
    location: {
      latitude: 0,
      longitude: 0
    },
    commonExpenses: 0,
    flats: [],
  }

  flatMockUp: FlatCreateRequest = {

    floor: 0,
    roomNumber: '1B',
    ownerAssignedId: '',
    totalRooms: 0,
    totalBaths: 0,
    hasTerrace: true
  };

  flatToInsert?: FlatCreateRequest

  availableOwners: Owner[] = []

  availableManagers: Manager[] = []

  constructor(private buildingService: BuildingService, private flatService: FlatService, private ownerService: OwnerService, private managerService: ManagerService, private router: Router, private route : ActivatedRoute) {
    this.obtainConstructionCompanyId();
    this.loadOwners();
    this.loadManagers();
  }

  createBuilding() {

    if (this.buildingToCreate.constructionCompanyId !== '' && this.buildingToCreate.managerId !== '') {
      this.buildingService.createBuilding(this.buildingToCreate)
        .subscribe({
          next: () => {
            alert("Building created with success!");
            this.router.navigateByUrl('buildings/list');
          },
          error: (errorMessage) => {
            alert(errorMessage.error);
          }
        })
    }
    else {
      alert("Please check that all select values has been selected");
    }
  }

  createFlat(): void {
    this.flatToInsert = this.flatToAddWithValues()
    if (this.flatToInsert.ownerAssignedId !== '') {
      this.flatService.createFlat(this.flatToInsert)
        .subscribe({
          next: () => {
            alert("Flat created with success");
            this.buildingToCreate.flats.push(this.flatToInsert!)
            this.resetFlatMockUpValues();
          },
          error: (errorMessage) => {
            alert(errorMessage.error);
            this.resetFlatMockUpValues();
          }
        })
    }
    else {
      alert("A owner must be selected");
    }

  }

  removeFlat(index: number): void {
    this.buildingToCreate.flats.splice(index, 1);
  }

  getFlatOwnerFirstname(ownerId?: string): string {
    return this.availableOwners.find(owner => owner.id === ownerId)?.firstname ?? "Not found"
  }

  private obtainConstructionCompanyId() : void
  {
    this.route.paramMap
    .subscribe({
      next:(params) => {
        this.buildingToCreate.constructionCompanyId = params.get('constructionCompanyId') ?? '';

        if(this.buildingToCreate.constructionCompanyId == ''){
          alert("Unprevisted error, redirecting.");
          this.router.navigateByUrl('/construction-companies/list');
        }
      }
    })

    
  }

  private flatToAddWithValues(): FlatCreateRequest {
    return {
      floor: this.flatMockUp.floor,
      roomNumber: this.flatMockUp.roomNumber,
      ownerAssignedId: this.flatMockUp.ownerAssignedId,
      totalRooms: this.flatMockUp.totalRooms,
      totalBaths: this.flatMockUp.totalBaths,
      hasTerrace: this.flatMockUp.hasTerrace
    };
  }

  private resetFlatMockUpValues(): void {

    this.flatMockUp.floor = 0,
      this.flatMockUp.roomNumber = '1A',
      this.flatMockUp.totalRooms = 0,
      this.flatMockUp.totalBaths = 0,
      this.flatMockUp.hasTerrace = true
  };

  private loadOwners(): void {
    this.ownerService.getOwners()
      .subscribe({
        next: (Response) => {
          this.availableOwners = Response;
        }
      });
  }

  private loadManagers(): void {
    this.managerService.getAllManagers()
      .subscribe({
        next: (Response) => {
          this.availableManagers = Response;
        }
      });
  }


  deployOwnerPopUp(): void {
    this.showOwnerCreation = true;
  }

  undeployOwnerPopup(): void {
    this.showOwnerCreation = false;
    this.loadOwners();
  }


  onChangeOwner(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const value = target?.value || '';
    this.flatMockUp.ownerAssignedId = value;
  }

  onChangeManager(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const value = target?.value || '';
    this.buildingToCreate.managerId = value;
  }

}
