import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BuildingService } from '../services/building.service';
import { FlatService } from '../../flat/services/flat.service';
import { OwnerService } from '../../owner/services/owner.service';
import { Owner } from '../../owner/interfaces/owner';
import { FlatCreateRequest } from '../../flat/interfaces/flat-create-request';
import { BuildingCreateRequest } from '../interfaces/building-create-request';

@Component({
  selector: 'app-building-create',
  templateUrl: './building-create.component.html',
  styleUrl: './building-create.component.css'
})
export class BuildingCreateComponent {

  showOwnerCreation: boolean = false;

  buildingToCreate: BuildingCreateRequest = {
    managerId: '00000000-0000-0000-0000-000000000000',
    constructionCompanyId: '00000000-0000-0000-0000-000000000000',
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
    ownerAssignedId: '00000000-0000-0000-0000-000000000000',
    totalRooms: 0,
    totalBaths: 0,
    hasTerrace: true
  };

  flatToInsert?: FlatCreateRequest

  availableOwners: Owner[] = []

  constructor(private buildingService: BuildingService, private flatService: FlatService, private ownerService: OwnerService, private router: Router) {
    this.loadOwners();
  }

  createBuilding() {
    this.ensureGuid(this.buildingToCreate.constructionCompanyId);
    this.ensureGuid(this.buildingToCreate.managerId);
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
    this.ensureGuid(this.flatToInsert.ownerAssignedId);
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
      this.flatMockUp.ownerAssignedId = '',
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

  deployOwnerPopUp(): void {
    this.showOwnerCreation = true;
  }

  undeployOwnerPopup(): void {
    this.showOwnerCreation = false;
    this.loadOwners();
  }


  onChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const value = target?.value || '';
    this.flatMockUp.ownerAssignedId = value;
  }

  isValidGuid(guid: string): boolean {
    const guidPattern = /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/;
    return guidPattern.test(guid);
  }

  ensureGuid(guid: string): string {
    if (this.isValidGuid(guid)) {
      return guid;
    }
    return '00000000-0000-0000-0000-000000000000';
  }
}
