import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscribable, Subscription, last } from 'rxjs';
import { InvitationService } from '../services/invitation.service';
import { Invitation } from '../interfaces/invitation';
import { StatusEnum } from '../interfaces/enums/status-enum';
import { Router } from '@angular/router';

@Component({
  selector: 'app-invitation-list',
  templateUrl: './invitation-list.component.html',
  styleUrl: './invitation-list.component.css'
})
export class InvitationListComponent  implements OnInit
{
    
  invitations? : Invitation[];
  
  constructor(private invitationService : InvitationService, private router : Router)
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
        return 'Accepted';
      case StatusEnum.Pending:
        return 'Pending';
      case StatusEnum.Rejected:
        return 'Rejected';
      default:
        return 'Unknown';
    }
  }

  createInvitation () : void 
  {
    this.router.navigateByUrl('invitations/create')
  }
  
  deleteInvitation(id : string) : void 
  {
    this.invitationService.deleteInvitation(id)
    .subscribe({
      next: () => {
        if(this.invitations)
          {
          this.invitations = this.invitations.filter(invitation => invitation.id !== id);
          }
        },
      error: (errorMessage) => {
        alert(errorMessage.error)
      }
    })
  }

  updateInvitation(id : string) : void
  {
    this.router.navigateByUrl(`invitations/update/${id}`);
  }

  checkIfItHasLastname(lastname : string) : string 
  {
    if(lastname == '')
    {
      return "-";
    }
    return lastname;
  }

}
