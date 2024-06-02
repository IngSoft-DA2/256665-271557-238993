import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscribable, Subscription } from 'rxjs';

@Component({
  selector: 'app-invitation-list',
  templateUrl: './invitation-list.component.html',
  styleUrl: './invitation-list.component.css'
})
export class InvitationListComponent  implements OnInit,OnDestroy
{
  
  subscription : Subscription;
  
  constructor()
  {
    
  }
  ngOnInit(): void 
  {
    this.subscription = this.invitationService.getAllInvitations()
    .subscribe({
      next: (Response) => {
        this.invitations$ = Response;
      },
    })
  }
  
  
  
  
  ngOnDestroy(): void 
  {
   this.subscription.unsubscribe;
  }

}
