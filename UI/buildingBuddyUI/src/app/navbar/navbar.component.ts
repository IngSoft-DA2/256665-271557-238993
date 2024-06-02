import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent 
{

  userRole : string

  constructor(private router : Router)
  {
    // We need to get the user role. Now I hardcoded it, so it can be show up. But is wrong.
    this.userRole = "Admin";
  }

  logout() : void 
  {
    // We need to logout the user when the button is clicked. Need to call service function here.

    this.router.navigateByUrl("/")
  }


}
