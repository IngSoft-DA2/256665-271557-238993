import { Component } from '@angular/core';
import { Building } from '../interfaces/building';
import { Router } from '@angular/router';
import { BuildingService } from '../services/building.service';
import { Manager } from '../../manager/interfaces/manager';

@Component({
  selector: 'app-building-list',
  templateUrl: './building-list.component.html',
  styleUrl: './building-list.component.css'
})
export class BuildingListComponent 
{
  
  buildings : Building[] = [];
  hasManager : boolean = false;

  constructor(private buildingService : BuildingService)
  {
    this.buildingService.getAllBuildings()
    .subscribe({
      next: (Response) => {
        this.buildings = Response
        console.log(this.buildings)
      }
    })
  }

  checkIfItHasManager(manager? : Manager) : string
  {
    return manager ? manager.name : 'No manager at the moment';
  }
}
