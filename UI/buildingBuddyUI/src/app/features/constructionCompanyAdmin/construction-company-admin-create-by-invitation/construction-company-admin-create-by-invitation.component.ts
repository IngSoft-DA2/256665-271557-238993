import { Component } from '@angular/core';
import { InvitationService } from '../../invitation/services/invitation.service';
import { Invitation } from '../../invitation/interfaces/invitation';
import { ActivatedRoute, Router } from '@angular/router';
import { constructionCompanyAdminCreateRequest } from '../interfaces/construction-company-admin-create-request';
import { ConstructionCompanyAdminService } from '../services/construction-company-admin.service';

@Component({
  selector: 'app-construction-company-admin-create-by-invitation', 
  templateUrl: './construction-company-admin-create-by-invitation.component.html', 
  styleUrls: ['./construction-company-admin-create-by-invitation.component.css']
})
export class ConstructionCompanyAdminCreateByInvitationComponent {

  invitationOfUser?: Invitation
  invitationId: string = '';

  constructionCompanyAdminToCreate: constructionCompanyAdminCreateRequest =
    {
      firstname: '',
      lastname: '',
      email: '',
      password: '',
      invitationId: undefined
    };

  constructor(private invitationService: InvitationService,
    private constructionCompanyAdminService: ConstructionCompanyAdminService, private router: Router, private route: ActivatedRoute) {
    this.route.queryParams.subscribe({
      next: (queryParams) => {
        this.invitationId = queryParams['idOfInvitationAccepted']
      }
    });

    this.invitationService.getInvitationById(this.invitationId)
      .subscribe({
        next: (Response) => {
          this.invitationOfUser = Response;

          this.constructionCompanyAdminToCreate =
          {
            firstname: this.invitationOfUser.firstname,
            lastname: this.invitationOfUser.lastname,
            email: this.invitationOfUser.email,
            password: '',
            invitationId: this.invitationOfUser.id
          };

        },
        error: (errorMessage) => {
          alert("Invitation was not found, redirecting...");
          this.router.navigateByUrl('/');
        }
      })
  }

  createConstructionCompanyAdmin(): void {
    if (this.invitationOfUser) {
      alert(this.constructionCompanyAdminToCreate.email)
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




