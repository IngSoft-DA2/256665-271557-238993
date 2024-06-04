import { Component } from '@angular/core';
import { ManagerService } from '../../services/manager.service';
import { Router } from '@angular/router';
import { Manager } from '../../interfaces/manager';

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
        console.log(this.managers[0])
      },
      error : (errorMessage) => {
        alert(errorMessage.error);
      }
    })
  }

  getBuildingsWhereManagerIsWorking(managerId : string) : void
  {
    this.router.navigateByUrl(`managers/${managerId}/buildings`);
    alert("To implement");
  }

  deleteManager(managerIdToDelete : string) : void
  {
    this.managerService.deleteManager(managerIdToDelete)
    .subscribe({
      next: () =>{
        alert("Manager deleted with success");
        this.managers = this.managers.filter(manager => manager.id !== managerIdToDelete)
      },
      error : (errorMessage) => {
        alert(errorMessage.error);
      }
    })
  }
}
