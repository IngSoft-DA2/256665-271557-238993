import { Component, OnInit } from '@angular/core';
import { invitationUpdateRequest } from '../interfaces/invitation-update';
import { ActivatedRoute, Router } from '@angular/router';
import { InvitationService } from '../services/invitation.service';
import { Invitation } from '../interfaces/invitation';
import { StatusEnum } from '../interfaces/enums/status-enum';
import { SystemUserRoleEnum } from '../interfaces/enums/system-user-role-enum';

@Component({
  selector: 'app-invitation-update',
  templateUrl: './invitation-update.component.html',
  styleUrl: './invitation-update.component.css'
})
export class InvitationUpdateComponent
{

  invitationFound? : Invitation;
  id: string | null = null;

  constructor(private invitationService : InvitationService , private router : Router, private route : ActivatedRoute) 
  {
    this.getInvitationById();
  }


  obtainId() : void
  {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
      }
    });
  }

  getInvitationById()
  {
    this.obtainId();

    if(this.id)
      this.invitationService.getInvitationById(this.id)
      .subscribe({
        next: (Response) => {
          this.invitationFound = Response;
        },
        
        error: (errorMessage) => {
          alert(errorMessage.error);
        }
      })
  }
  
  updateInvitation() : void
  {
    if(this.id && this.invitationFound)
    {
      const invitationWithUpdates : invitationUpdateRequest = {
        status: this.invitationFound.status,
        expirationDate: this.invitationFound?.expirationDate
      };

      this.invitationService.updateInvitation(this.id,invitationWithUpdates)
      .subscribe({
        next: () => {
          this.router.navigateByUrl('invitations/list');
        },
        error: (errorMessage) => {
          alert(errorMessage.error);
        }
      })
    }
    
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
}

