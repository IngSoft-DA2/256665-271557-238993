import { Component } from '@angular/core';
import { Building } from '../interfaces/building';
import { BuildingService } from '../services/building.service';
import { Manager } from '../../manager/interfaces/manager';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-building-list',
  templateUrl: './building-list.component.html',
  styleUrl: './building-list.component.css'
})
export class BuildingListComponent {

  buildings: Building[] = [];
  hasManager: boolean = false;
  userId: string = '';

  constructor(private buildingService: BuildingService, private router: Router, private route: ActivatedRoute) {

    this.route.queryParams
      .subscribe({
        next: (queryParams) => {
          this.userId = queryParams['userId'];
        }
      })

    if (this.userId !== undefined) {
      this.buildingService.getAllBuildings(this.userId)
        .subscribe({
          next: (Response) => {
            this.buildings = Response
            console.log(this.buildings)
          }
        })
    }
    else {
      alert("User was not found, redirecting")
      this.router.navigateByUrl('/login');
    }

  }

  checkIfItHasManager(manager?: Manager): string {
    return manager ? manager.name : 'No manager at the moment';
  }


  deleteBuilding(buildingId: string): void {
    this.buildingService.deleteBuilding(buildingId)
      .subscribe({
        next: () => {
          this.buildings = this.buildings.filter(b => b.id !== buildingId);
        },
        error: (errorMessage) => {
          alert("Cannot delete this building, communicate with an admin")
        }
      })
  }
}
