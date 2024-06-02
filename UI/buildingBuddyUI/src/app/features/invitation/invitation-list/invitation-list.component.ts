import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscribable, Subscription } from 'rxjs';
import { InvitationService } from '../services/invitation.service';
import { Invitation } from '../interfaces/invitation';
import { StatusEnum } from '../interfaces/enums/status-enum';

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

  getStatusString(status: number): string {
    switch (status) {
      case StatusEnum.Accepted:
        return 'Pending';
      case StatusEnum.Pending:
        return 'Accepted';
      case StatusEnum.Rejected:
        return 'Rejected';
      default:
        return 'Unknown';
    }
  
  }

}
