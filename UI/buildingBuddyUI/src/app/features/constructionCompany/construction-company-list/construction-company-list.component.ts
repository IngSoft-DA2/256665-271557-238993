import { Component } from '@angular/core';
import { ConstructionCompany } from '../interfaces/construction-company';
import { Router } from '@angular/router';
import { ConstructionCompanyService } from '../services/construction-company.service';
import { ConstructionCompanyAdminService } from '../../constructionCompanyAdmin/services/construction-company-admin.service';
import { ConstructionCompanyAdmin } from '../../constructionCompanyAdmin/interfaces/construction-company-admin';
import { SystemUserRoleEnum } from '../../invitation/interfaces/enums/system-user-role-enum';

@Component({
  selector: 'app-construction-company-list',
  templateUrl: './construction-company-list.component.html',
  styleUrls: ['./construction-company-list.component.css']
})
export class ConstructionCompanyListComponent {
  // I declared it with values because this is a temporal variable, it is only to test if this works.
  // We need to pass the logged user between components.
  
  private constructionCompanyTest: ConstructionCompany = {
    id: '123123adawsfsfs',
    name: 'Construction Company 1',
    userCreatorId: 'ad23213da123dassacfrsgth',
    buildingsId: []
  };
  
  constructionCompanyAdminObtainedFromLogin : ConstructionCompanyAdmin = {
    id : 'ad23213da123dassacfrsgth',
    firstname : 'testFirstname',
    lastname : 'testLastname',
    email : 'test@gmail.com',
    password : 'test2003!',
    role : SystemUserRoleEnum.ConstructionCompanyAdmin,
    constructionCompany : this.constructionCompanyTest
  };
  // <-------- Removed things from above when we have the user passed down to here... --------->

  constructionCompanyOfUser? : ConstructionCompany;

  constructor(private constructionCompanyService: ConstructionCompanyService, private router: Router) {
    this.setProperties();
  }

  checkIfItHasBuildings(): void {
    if (this.constructionCompanyOfUser?.buildingsId && this.constructionCompanyOfUser.buildingsId.length > 0) {
      this.router.navigateByUrl('buildings/list');
    } else {
      alert('No buildings at the moment');
    }
  }

  private setProperties() {
    if (this.constructionCompanyAdminObtainedFromLogin.constructionCompany !== undefined) {
      this.constructionCompanyOfUser = {
        id: this.constructionCompanyAdminObtainedFromLogin.constructionCompany.id,
        name: this.constructionCompanyAdminObtainedFromLogin.constructionCompany.name,
        userCreatorId: this.constructionCompanyAdminObtainedFromLogin.constructionCompany.userCreatorId,
        buildingsId: this.constructionCompanyAdminObtainedFromLogin.constructionCompany.buildingsId,
      };
    }
  }
}
