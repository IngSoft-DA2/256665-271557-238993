import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { SystemUserRoleEnum } from "../../features/invitation/interfaces/enums/system-user-role-enum";
import { User } from "../../features/login/interfaces/user";
import { LoginService } from "../../features/login/services/login.service";
import { HttpParams } from "@angular/common/http";


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  InLoginPage: boolean = false;
  userConnected?: User = undefined;
  SystemUserRoleEnumValues = SystemUserRoleEnum;

  constructor(private loginService: LoginService, private router: Router) {
    // We need to get the user role. Now I hardcoded it, so it can be show up. But is wrong.

    loginService.getUser().subscribe({
      next: (Response) => {
        this.userConnected = Response
        console.log("Usuario encontrado, valores: " + this.userConnected)
      },
      error: () => {
        this.userConnected = undefined;
      }
    })


  }

  goToLoginPage(): void {
    this.InLoginPage = true;
    this.router.navigateByUrl('/login');
  }

  logout(): void {
    this.loginService.removeUserConnected()
    this.router.navigateByUrl('/');
    this.InLoginPage = false;
  }

  goToBuildingCustomList(): void {
    const queryParams = new HttpParams()
    .set('managerId', this.userConnected?.userId ?? '');

    this.router.navigateByUrl('/buildings/list?${queryParams}');

    this.router.navigateByUrl(`/buildings/list?${queryParams}`);
  }
}
