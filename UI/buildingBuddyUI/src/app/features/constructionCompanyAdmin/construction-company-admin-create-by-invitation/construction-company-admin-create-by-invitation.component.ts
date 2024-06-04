import { Component } from '@angular/core';
import { InvitationService } from '../../invitation/services/invitation.service';
import { Invitation } from '../../invitation/interfaces/invitation';
import { ActivatedRoute, Router } from '@angular/router';
import { constructionCompanyAdminCreateRequest } from '../interfaces/construction-company-admin-create-request';
import { ConstructionCompanyAdminService } from '../services/construction-company-admin.service';
import { SystemUserRoleEnum } from '../../invitation/interfaces/enums/system-user-role-enum';
import { StatusEnum } from '../../invitation/interfaces/enums/status-enum';

@Component({
  selector: 'app-construction-company-admin-create-by-invitation',
  templateUrl: './construction-company-admin-create-by-invitation.component.html',
  styleUrls: ['./construction-company-admin-create-by-invitation.component.css']
})
export class ConstructionCompanyAdminCreateByInvitationComponent {

  invitationOfUser?: Invitation
  hasInvitation : boolean = false;
  hasValidRole : boolean = true;
  
  invitationId: string = '';
  userRole?: SystemUserRoleEnum; //To do: We need to implement how to get the user role.

  constructionCompanyAdminToCreate: constructionCompanyAdminCreateRequest =
    {
      firstname: '',
      lastname: '',
      email: '',
      password: '',
      invitationId: undefined
    };

  constructor(
    private invitationService: InvitationService,
    private constructionCompanyAdminService: ConstructionCompanyAdminService,
    private router: Router, private route: ActivatedRoute
  ) {

    this.route.queryParams.subscribe({
      next: (queryParams) => {
        this.invitationId = queryParams['idOfInvitationAccepted']      }
    });

    if (this.invitationId !== undefined) {
      this.invitationService.getInvitationById(this.invitationId)
        .subscribe({
          next: (Response) => {
            this.invitationOfUser = Response;
            if (this.invitationOfUser.status === StatusEnum.Pending) {
              this.constructionCompanyAdminToCreate =
              {
                firstname: this.invitationOfUser.firstname,
                lastname: this.invitationOfUser.lastname,
                email: this.invitationOfUser.email,
                password: '',
                invitationId: this.invitationOfUser.id
              };
              this.hasInvitation = true;
            }
            else{
              alert("Invitation was found, but it status needs to be pending. Redirecting...");
              this.router.navigateByUrl('/');
            }

          },
          error: () => {
            alert("Invitation was not found, redirecting...");
            this.router.navigateByUrl('/');
          }
        })
    }
    else if (this.userRole === SystemUserRoleEnum.ConstructionCompanyAdmin) {
      this.hasValidRole = true;
      
    }
    else{
      alert("You do not have the necessary role to enter here, redirecting");
      this.router.navigateByUrl('/');
    }

  }

  createConstructionCompanyAdmin(): void {
    if (this.invitationOfUser || this.userRole == SystemUserRoleEnum.ConstructionCompanyAdmin) {
      this.constructionCompanyAdminService.createConstructionCompanyAdmin(this.constructionCompanyAdminToCreate)
        .subscribe({
          next: () => {
            alert("You are now a construction company admin!");
            this.router.navigateByUrl('/');
          },
          error(errorMessage) {
            alert(errorMessage.error);
          }
        });
    }
    else {
      alert("It needs an invitation to be possible to create a construction company admin.")
    }
  }
}




