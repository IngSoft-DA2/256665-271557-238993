import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent 
{

  constructor(private router : Router){}

  logout() : void 
  {
    // We need to logout the user when the button is clicked.

    this.router.navigateByUrl("/")
  }


}
