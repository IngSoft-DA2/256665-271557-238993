import { Component } from '@angular/core';
import { Invitation } from '../interfaces/invitation';
import { InvitationService } from '../services/invitation.service';
import { ActivatedRoute } from '@angular/router';
import { StatusEnum } from '../interfaces/enums/status-enum';
import { SystemUserRoleEnum } from '../interfaces/enums/system-user-role-enum';
import { invitationUpdateRequest } from '../interfaces/invitation-update';

@Component({
  selector: 'app-invitation-list-by-email',
  templateUrl: './invitation-list-by-email.component.html',
  styleUrl: './invitation-list-by-email.component.css'
})
export class InvitationListByEmailComponent 
{

  email: string | undefined;
  invitationsOfEmail?: Invitation[]
  password: string = '';

  constructor(private invitationService: InvitationService, private route: ActivatedRoute) {
    this.route.queryParams.subscribe(params => {
      this.email = params['email'];
    });

    if (this.email) {
      this.invitationService.getInvitationByEmail(this.email)
        .subscribe({
          next: (Response) => {
            this.invitationsOfEmail = Response;
          },
          error: (errorMessage) => {
            alert(errorMessage.error);
          }
        })

    }
  }

  acceptInvitation(invitation : Invitation) : void
  {
    
    if(invitation.role == SystemUserRoleEnum.ConstructionCompanyAdmin)
    {
      const constructionCompanyAdminService : ConstructionCompanyAdminService;
      const constructionCompanyAdminToCreate : constructionCompanyAdminCreateRequest = 
      {
        firstname = invitation.firstname,
        lastname = invitation.lastname,
        email = invitation.email,
        password =  this.password,
        role = invitation.role
        invitationId = invitation.id
      };
      constructionCompanyAdminService.createConstructionCompanyAdmin(constructionCompanyAdminToCreate);
    }
    else if(invitation.role == SystemUserRoleEnum.Manager)
    {
      const managerService : ManagerService;
      const managerToCreate : ManagerCreateRequest = new 
      {
          email : invitation.email,
          this.password : this.password
      };
      managerService.createManager(managerToCreate);
    }
    else
    {
      alert("The only roles that can have at the moment a invitation are Manager role and Construction Company Admin Role")
    }

  }

  rejectInvitation(invitation : Invitation) : void
  {
    const invitationRejected: invitationUpdateRequest = 
    {
      status: StatusEnum.Rejected,
      expirationDate: invitation.expirationDate
    };
    
    this.invitationService.updateInvitation(invitation.id, invitationRejected)
  }


  getSystemUserRoleString(role: number): string {
    switch (role) {
      case SystemUserRoleEnum.Manager:
        return 'Manager';
      case SystemUserRoleEnum.ConstructionCompanyAdmin:
        return 'Construction Company Admin';
      default:
        return 'Unknown';
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
