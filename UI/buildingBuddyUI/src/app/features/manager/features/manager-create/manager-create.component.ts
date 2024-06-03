import { Component } from '@angular/core';
import { InvitationService } from '../../../invitation/services/invitation.service';
import { ManagerService } from '../../services/manager.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Invitation } from '../../../invitation/interfaces/invitation';
import { ManagerCreateRequest } from '../../interfaces/manager-create-request';
import { invitationUpdateRequest } from '../../../invitation/interfaces/invitation-update';
import { StatusEnum } from '../../../invitation/interfaces/enums/status-enum';

@Component({
  selector: 'app-manager-create',
  templateUrl: './manager-create.component.html',
  styleUrl: './manager-create.component.css'
})
export class ManagerCreateComponent 
{
  invitationToOperate? : Invitation;
  passwordSelected : string = '';
  invitationId : string = '';
  wantsToCreate : boolean = false;

  constructor(private invitationService : InvitationService, private managerService : ManagerService, private route : ActivatedRoute, private router : Router)
  {
    this.route.queryParams.subscribe({
      next: (queryParams) => {
        this.invitationId = queryParams['id']
      } 
    });
    
    if(this.invitationId)
    {
      this.invitationService.getInvitationById(this.invitationId)
      .subscribe({
        next : (Response) => 
        {
          this.invitationToOperate = Response;
        },
      error : (errorMessage) => 
      {
        alert(errorMessage.error);
      }
      });
    }
  }

  createManager() : void
  {
    if(this.invitationToOperate)
    {
      const managerToCreate : ManagerCreateRequest = 
      {
        email : this.invitationToOperate.email,
        password : this.passwordSelected
      }
      this.managerService.createManager(managerToCreate)
      .subscribe({
        next: (Response) => {
          alert("You are now a manager");
          this.router.navigateByUrl('invitations/list');
        }
      })
    }
    else
    {
      alert("It needs an invitation to be possible to create a manager.")
    }  
  }
}
