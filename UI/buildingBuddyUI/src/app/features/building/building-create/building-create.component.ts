import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BuildingService } from '../services/building.service';
import { CreateBuildingRequest } from '../interfaces/building-create-request';
import { CreateFlatRequest } from '../../flat/interfaces/flat-create-request';
import { FlatService } from '../../flat/services/flat.service';
import { Flat } from '../../flat/interfaces/flat';
import { OwnerService } from '../../owner/services/owner.service';

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
    roomNumber: '',
    ownerAssignedId: '',
    totalRooms: 0,
    totalBaths: 0,
    hasTerrace: true
  };


  constructor(private buildingService: BuildingService, private flatService : FlatService, private ownerService: OwnerService, private router: Router) {

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

  removeFlat(positionOnArrayOfFlatToDelete : number) : void 
  {
    const flatToRemove = this.buildingToCreate.flats[positionOnArrayOfFlatToDelete];
    this.buildingToCreate.flats = this.buildingToCreate.flats.filter(flat => flat !== flatToRemove);
  }

  private resetFlatValues(): void {

    this.flatToCreate.floor = 0,
    this.flatToCreate.roomNumber = '',
    this.flatToCreate.ownerAssignedId = '',
    this.flatToCreate.totalRooms = 0,
    this.flatToCreate.totalBaths = 0,
    this.flatToCreate.hasTerrace = true
  };

  getFlatOwnerName(ownerId : string) : string
  {
    var ownerName = '';

    this.ownerService.getOwnerById(ownerId)
    .subscribe({
      next: (Response) => {
        ownerName = Response.firstname
      }
    })
    return ownerName;
  }

}

