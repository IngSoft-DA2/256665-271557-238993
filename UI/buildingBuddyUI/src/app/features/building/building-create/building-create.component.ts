import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BuildingService } from '../services/building.service';
import { CreateBuildingRequest } from '../interfaces/building-create-request';
import { CreateFlatRequest } from '../../flat/interfaces/flat-create-request';
import { FlatService } from '../../flat/services/flat.service';
import { Flat } from '../../flat/interfaces/flat';
import { OwnerService } from '../../owner/services/owner.service';
import { Owner } from '../../owner/interfaces/owner';

@Component({
  selector: 'app-building-create',
  templateUrl: './building-create.component.html',
  styleUrl: './building-create.component.css'
})
export class BuildingCreateComponent {

  buildingToCreate: CreateBuildingRequest = {
    managerId: '',
    constructionCompanyId: '',
    name: '',
    address: '',
    location: {
      latitude: 0,
      longitude: 0,
    },
    commonExpenses: 0,
    flats: [],
  }

  flatToCreate: CreateFlatRequest = {

    floor: 0,
    roomNumber: '1B',
    ownerAssignedId: '',
    totalRooms: 0,
    totalBaths: 0,
    hasTerrace: true
  };

  availableOwners: Owner[] = []


  constructor(private buildingService: BuildingService, private flatService: FlatService, private ownerService: OwnerService, private router: Router) {
    
  }

  createBuilding() {
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

  createFlat(): void {
    this.flatService.createFlat(this.flatToCreate)
      .subscribe({
        next: () => {
          alert("Flat created with success");
          this.buildingToCreate.flats.push(this.flatToCreate)
          this.resetFlatValues();
        }
      })
  }

  removeFlat(index: number): void {
    this.buildingToCreate.flats.splice(index, 1);
  }

  private resetFlatValues(): void {

    this.flatToCreate.floor = 0,
      this.flatToCreate.roomNumber = '',
      this.flatToCreate.ownerAssignedId = '',
      this.flatToCreate.totalRooms = 0,
      this.flatToCreate.totalBaths = 0,
      this.flatToCreate.hasTerrace = true
  };

  getFlatOwnerName(ownerId: string): string {
    var ownerName = '';

    this.ownerService.getOwnerById(ownerId)
      .subscribe({
        next: (Response) => {
          ownerName = Response.firstname;
        },
        error: () =>{
          ownerName = 'Not has';
        }
      })
    return ownerName;
  }


  onChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const value = target?.value || '';
    this.flatToCreate.ownerAssignedId = value;
  }

}

