import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { SystemUserRoleEnum } from "../interfaces/enums/system-user-role-enum";
import { invitationCreateRequest } from "../interfaces/invitation-create";
import { InvitationService } from "../services/invitation.service";


@Component({
  selector: 'app-invitation-create',
  templateUrl: './invitation-create.component.html',
  styleUrl: './invitation-create.component.css'
})
export class InvitationCreateComponent {


  invitationToCreate: invitationCreateRequest;
  selectedRole: string;
  roleKeys: number[] = [SystemUserRoleEnum.Manager, SystemUserRoleEnum.ConstructionCompanyAdmin];



  constructor(private invitationService: InvitationService, private router: Router) {
    this.selectedRole = "Select a role";
    this.invitationToCreate =
    {
      firstname: '',
      lastname: '',
      email: '',
      expirationDate: new Date(new Date().setDate(new Date().getDate() + 1)),
      role: SystemUserRoleEnum.Manager
    }
  }

  createInvitation() {
    if (this.selectedRole != "Select a role") {

      this.invitationToCreate.role = Number(this.selectedRole);
      this.invitationService.createInvitation(this.invitationToCreate)
        .subscribe({
          next: () => {
            this.router.navigateByUrl('invitations/list');
          },
          error: (errorMessage) => {
            alert(errorMessage.error);
            this.selectedRole = "Select a role"
          }
        })
    }
    else
    {
      alert("You must select a role from the list");
    }
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


}
