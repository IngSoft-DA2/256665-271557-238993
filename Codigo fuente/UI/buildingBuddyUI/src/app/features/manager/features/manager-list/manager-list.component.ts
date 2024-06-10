import { Component } from '@angular/core';
import { ManagerService } from '../../services/manager.service';
import { Router } from '@angular/router';
import { Manager } from '../../interfaces/manager';
import { HttpParams } from '@angular/common/http';
import { LoginService } from '../../../login/services/login.service';
import { User } from '../../../login/interfaces/user';
import { SystemUserRoleEnum } from '../../../invitation/interfaces/enums/system-user-role-enum';

@Component({
  selector: 'app-manager-list',
  templateUrl: './manager-list.component.html',
  styleUrl: './manager-list.component.css'
})



export class ManagerListComponent 
{
  managers : Manager[] = [];
  userConnected?: User = undefined;
  SystemUserRoleEnumValues = SystemUserRoleEnum;

  constructor(private managerService : ManagerService, private router : Router, private loginService : LoginService)
  {
    loginService.getUser().subscribe({
      next: (response) => {
        this.userConnected = response;
        console.log("Usuario encontrado, valores: " + this.userConnected);
      },
      error: () => {
        this.userConnected = undefined;
      }
    });
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
    const queryParams = new HttpParams()
    .set('managerId', managerId);
    this.router.navigateByUrl(`buildings/list?${queryParams}`);
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
