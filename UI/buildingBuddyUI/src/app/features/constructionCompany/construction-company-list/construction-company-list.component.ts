import { Component } from '@angular/core';
import { ConstructionCompany } from '../interfaces/construction-company';
import { Router } from '@angular/router';
import { ConstructionCompanyAdmin } from '../../constructionCompanyAdmin/interfaces/construction-company-admin';
import { SystemUserRoleEnum } from '../../invitation/interfaces/enums/system-user-role-enum';
import { User } from '../../login/interfaces/user';
import { ConstructionCompanyService } from '../services/construction-company.service';
import { LoginService } from '../../login/services/login.service';
import { ConstructionCompanyAdminService } from '../../constructionCompanyAdmin/services/construction-company-admin.service';

@Component({
  selector: 'app-construction-company-list',
  templateUrl: './construction-company-list.component.html',
  styleUrls: ['./construction-company-list.component.css']
})
export class ConstructionCompanyListComponent {
  userLogged?: User;
  constructionCompanyOfUser: ConstructionCompany | undefined = undefined;

  constructor(private constructionCompanyAdminService: ConstructionCompanyAdminService, private loginService: LoginService, private router: Router) {

    this.loginService.getUser()
      .subscribe({
        next: (Response) => {
          this.userLogged = Response;

          if (this.userLogged !== undefined) {
            this.constructionCompanyAdminService.getConstructionCompanyAdmin(this.userLogged.userId)
            .subscribe({
              next : (Response) => {
                this.constructionCompanyOfUser = Response.constructionCompany
                console.log(this.constructionCompanyOfUser);
              }
            })
          }
          else {
            alert("User was not found, redirecting");
            this.router.navigateByUrl('/login');
          }
        },
        error: () => {
          alert("User was not found, redirecting");
          this.router.navigateByUrl('/login');
        }
      })
      console.log(this.constructionCompanyOfUser);
  }

}
