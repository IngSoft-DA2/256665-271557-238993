import { Component } from '@angular/core';
import { Building } from '../interfaces/building';
import { Router } from '@angular/router';

@Component({
  selector: 'app-building-list',
  templateUrl: './building-list.component.html',
  styleUrl: './building-list.component.css'
})
export class BuildingListComponent 
{
  
  buildings : Building[] = [];

  hasManager : boolean = false;

  constructor(private buildingService : BuildingService,private router : Router)
  {

  }
}
