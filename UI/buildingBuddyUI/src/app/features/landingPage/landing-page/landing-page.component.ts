import { HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.css'
})
export class LandingPageComponent
{
  emailToSearch : string = '';

  constructor(private router : Router){}


  showUpInvitations() : void
  {
    const queryParams = new HttpParams().set('email',this.emailToSearch);
    this.router.navigateByUrl(`invitations/guests/list?${queryParams}`)
  }
}
