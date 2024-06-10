import { Component } from "@angular/core";
import { SystemUserRoleEnum } from "../../features/invitation/interfaces/enums/system-user-role-enum";
import { User } from "../../features/login/interfaces/user";
import { LoginService } from "../../features/login/services/login.service";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent 
{

  userLogged : User | undefined = undefined;

  constructor(loginService : LoginService)
  {
    loginService.getUser()
    .subscribe({
      next : (Response) => {
        this.userLogged = Response
      }
    })
  }

  getSystemUserRoleString(role: number): string {
    switch (role) {
      case SystemUserRoleEnum.Admin:
        return 'ADMIN'
      case SystemUserRoleEnum.Manager:
        return 'MANAGER';
        case SystemUserRoleEnum.RequestHandler:
          return 'REQUEST HANDLER'
      case SystemUserRoleEnum.ConstructionCompanyAdmin:
        return 'CONSTRUCTION COMPANY ADMIN ';
      default:
        return '';
    }
  }



}