import { Component } from '@angular/core';
import { Invitation } from '../interfaces/invitation';
import { InvitationService } from '../services/invitation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StatusEnum } from '../interfaces/enums/status-enum';
import { SystemUserRoleEnum } from '../interfaces/enums/system-user-role-enum';
import { invitationUpdateRequest } from '../interfaces/invitation-update';
import { ManagerCreateRequest } from '../../manager/interfaces/manager-create-request';
import { ManagerService } from '../../manager/services/manager.service';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-invitation-list-by-email',
  templateUrl: './invitation-list-by-email.component.html',
  styleUrl: './invitation-list-by-email.component.css'
})
export class InvitationListByEmailComponent {

  email?: string;
  invitationsOfEmail?: Invitation[]
  hasInvitations: boolean = false;
  password: string = '';

  constructor(private invitationService: InvitationService,
    private router: Router, private route: ActivatedRoute) {


    this.route.queryParams.subscribe(params => {
      this.email = params['email'];
    });

    if (this.email) {
      this.invitationService.getInvitationsByEmail(this.email)
        .subscribe({
          next: (Response) => {
            this.invitationsOfEmail = Response;

            if (this.invitationsOfEmail.length > 0) {
              this.hasInvitations = true;
            }
          },
          error: (errorMessage) => {
            alert(errorMessage.error);
          }
        });
    }
  }

  acceptInvitation(invitation: Invitation): void {

    const queryParams = new HttpParams().set('idOfInvitationAccepted', invitation.id);

    if (invitation.role == SystemUserRoleEnum.Manager) {
      this.router.navigateByUrl(`managers/create?${queryParams}`)
    }
    else if (invitation.role == SystemUserRoleEnum.ConstructionCompanyAdmin) {
      this.router.navigateByUrl(`/constructionCompanyAdmin/create?${queryParams}`)
    }
    else {
      alert("The only roles that can have at the moment a invitation are Manager role and Construction Company Admin Role")
    }

  }

  rejectInvitation(invitation: Invitation): void {
    const invitationRejected: invitationUpdateRequest =
    {
      status: StatusEnum.Rejected,
      expirationDate: invitation.expirationDate
    };

    this.invitationService.updateInvitation(invitation.id, invitationRejected)
      .subscribe({
        next: (Response) => {
          alert("Rejected invitation with success");
          if (this.invitationsOfEmail) {
            const index = this.invitationsOfEmail.findIndex(inv => inv.id === invitation.id);
            if (index !== -1) {
              this.invitationsOfEmail[index].status = StatusEnum.Rejected;
            }
          }
        },
        error(errorMessage) {
          alert(errorMessage.error);
        }
      });
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
