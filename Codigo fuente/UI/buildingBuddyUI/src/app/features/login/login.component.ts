import { Component } from '@angular/core';
import { LoginService } from './services/login.service';
import { Router } from '@angular/router';
import { LoginRequest } from './interfaces/login-request';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent
{

  loginRequest : LoginRequest = {
    email : '',
    password : ''
  };

  constructor(private loginService : LoginService, private router : Router)
  {
  }

  login() : void
  {
    this.loginService.login(this.loginRequest)
    .subscribe({
      next: (Response) =>{
        this.loginService.storageUserValues(this.loginRequest,Response);
        this.router.navigateByUrl("home");
      },
      error: () => {
        alert("Imposible to login, try again later.")
        this.router.navigateByUrl("/")
      }
    })
  }

}
