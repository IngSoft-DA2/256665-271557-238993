import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BuildingService } from '../services/building.service';
import { CreateBuildingRequest } from '../interfaces/building-create-request';
import { FlatService } from '../../flat/services/flat.service';
import { OwnerService } from '../../owner/services/owner.service';
import { Owner } from '../../owner/interfaces/owner';
import { OwnerCreateRequest } from '../../owner/interfaces/owner-create-request';
import { FlatCreateRequest } from '../../flat/interfaces/flat-create-request';

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

  ownerToCreate: OwnerCreateRequest = {
    firstname : '',
    lastname : '',
    email : '',
    password : ''
  }

  flatMockUp: FlatCreateRequest = {

    floor: 0,
    roomNumber: '1B',
    ownerAssignedId: undefined,
    totalRooms: 0,
    totalBaths: 0,
    hasTerrace: true
  };

  flatToInsert? : FlatCreateRequest 

  availableOwners: Owner[] = []

  showOwnerCreation : boolean = false;


  constructor(private buildingService: BuildingService, private flatService: FlatService, private ownerService: OwnerService, private router: Router) {
    this.loadOwners();
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
    this.flatToInsert = this.flatToAddWithValues()
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

  removeFlat(index: number): void {
    this.buildingToCreate.flats.splice(index, 1);
  }

  createOwner(){
    
    this.ownerService.createOwner(this.ownerToCreate)
    .subscribe({
      next: () => {
        alert('Owner create with sucess')
        this.loadOwners();
        this.resetOwnerValues();
      }
    })
  }

  getFlatOwnerFirstname(ownerId?: string): string {
    return this.availableOwners.find(owner => owner.id === ownerId)?.firstname ?? "Not found"
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
      this.flatMockUp.ownerAssignedId = undefined,
      this.flatMockUp.totalRooms = 0,
      this.flatMockUp.totalBaths = 0,
      this.flatMockUp.hasTerrace = true
  };

  private resetOwnerValues(): void {

    this.ownerToCreate.firstname = '';
    this.ownerToCreate.lastname = '';
    this.ownerToCreate.email = '';
    this.ownerToCreate.password = '';
  };

  private loadOwners(): void {
    this.ownerService.getOwners()
      .subscribe({
        next: (Response) => {
          this.availableOwners = Response;
        }
      });
  }

  deployOwnerPopUp()
  {
    this.showOwnerCreation = true;
  }

  undeployOwnerPopup()
{
  this.showOwnerCreation = false;
}

  onChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const value = target?.value || '';
    this.flatMockUp.ownerAssignedId = value;
  }

}

