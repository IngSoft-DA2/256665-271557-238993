import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Invitation } from "../../../invitation/interfaces/invitation";
import { InvitationService } from "../../../invitation/services/invitation.service";
import { ManagerCreateRequest } from "../../interfaces/manager-create-request";
import { ManagerService } from "../../services/manager.service";


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
  executeCreation : boolean = false;
  managerToCreate : ManagerCreateRequest = 
  {
    email : '',
    password : ''
  };

  constructor(private invitationService : InvitationService, private managerService : ManagerService, private route : ActivatedRoute, private router : Router)
  {
    this.route.queryParams.subscribe({
      next: (queryParams) => {
        this.invitationId = queryParams['idOfInvitationAccepted']
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

  wantsToCreate() : void
  {
    this.executeCreation = true;
  }

  createManager() : void
  {
    if(this.invitationToOperate)
    { 
      this.managerService.createManager(this.managerToCreate, this.invitationId)
      .subscribe({
        next: () => {
          alert("You are now a manager");
          this.router.navigateByUrl('/login');
        },
        error(errorMessage)
        {
          alert(errorMessage.error);
        }
      });
    }
    else
    {
      alert("It needs an invitation to be possible to create a manager.")
    }  
  }
}
