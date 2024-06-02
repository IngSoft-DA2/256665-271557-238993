import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscribable, Subscription } from 'rxjs';
import { InvitationService } from '../services/invitation.service';
import { Invitation } from '../interfaces/invitation';

@Component({
  selector: 'app-invitation-list',
  templateUrl: './invitation-list.component.html',
  styleUrl: './invitation-list.component.css'
})
export class InvitationListComponent  implements OnInit
{
    
  invitations? : Invitation[];
  
  constructor(private invitationService : InvitationService)
  {
    
  }
  
  ngOnInit(): void 
  {
    this.invitationService.getAllInvitations()
    .subscribe({
      next: (Response) => {
        this.invitations = Response;
      },
    })
  }
  
}
