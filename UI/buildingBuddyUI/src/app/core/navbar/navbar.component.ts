import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { LoginService } from '../../features/login/services/login.service';
import { User } from '../../features/login/interfaces/user';
import { SystemUserRoleEnum } from '../../features/invitation/interfaces/enums/system-user-role-enum';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent
{
  InLoginPage : boolean = false;
  userConnected? : User = undefined;
  SystemUserRoleEnumValues = SystemUserRoleEnum;
  
  constructor(private loginService : LoginService, private router : Router)
  {
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

  goToLoginPage() : void
  {
    this.InLoginPage = true;
    this.router.navigateByUrl('/login');
  }

  logout() : void 
  {
    // We need to logout the user when the button is clicked. Need to call service function here.
    
    this.router.navigateByUrl("/")
  }


}
