import { Component } from '@angular/core';
import { ManagerService } from '../../services/manager.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manager-list',
  templateUrl: './manager-list.component.html',
  styleUrl: './manager-list.component.css'
})



export class ManagerListComponent 
{
  managers : Manager[] = [];

  constructor(private managerService : ManagerService, private router : Router)
  {
    this.getAllManagers();
  }


  private getAllManagers() : void 
  {
    this.managerService.getAllManagers()
    .subscribe({
      next : (Response) => {
        this.managers = Response;
      },
      error : (errorMessage) => {
        alert(errorMessage.error);
      }
    })
  }
}
