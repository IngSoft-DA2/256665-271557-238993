import { Component, OnInit } from '@angular/core';
import { OwnerService } from '../services/owner.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Owner } from '../interfaces/owner';
import { BuildingService } from '../../building/services/building.service';
import { Building } from '../../building/interfaces/building';

@Component({
  selector: 'app-owner-list',
  templateUrl: './owner-list.component.html',
  styleUrl: './owner-list.component.css'
})
export class OwnerListComponent{

  ownersOfBuilding: Owner[] = []
  buildingId: string = '';
  buildingSelected?: Building;

  constructor(private buildingService: BuildingService, private router: Router, private route: ActivatedRoute) {

    this.obtainBuildingIdFromRoute();
    this.buildingService.getBuildingById(this.buildingId)
      .subscribe({
        next: (Response) => {
          this.buildingSelected = Response;
          this.obtainAllOwners();
        },
        error: (errorMessage) => {
          alert(errorMessage.error + ", redirecting");
          this.router.navigateByUrl('/home') //Maybe into buildings/list?
        }
      })
  }



  private obtainBuildingIdFromRoute() {
    this.route.paramMap
      .subscribe({
        next: (params) => {
          this.buildingId = params.get('buildingId') ?? '';
          if (this.buildingId === '') {
            alert("Building was not found, redirecting to building list.");
            this.router.navigateByUrl('buildings/list');
          }
        }
      });
  }

  private obtainAllOwners() {
   
    if (this.buildingSelected) {
    
      this.ownersOfBuilding = [];

      this.buildingSelected.flats.forEach(flat => {

        if (flat.ownerAssigned && !this.ownersOfBuilding.find(owner => owner.id === flat.ownerAssigned.id)) {
          this.ownersOfBuilding.push(flat.ownerAssigned);
        }
      });
    }
  }

}
