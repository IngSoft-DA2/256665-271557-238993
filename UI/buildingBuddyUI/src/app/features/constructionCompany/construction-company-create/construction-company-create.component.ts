import { Component } from '@angular/core';
import { ConstructionCompanyCreateRequest } from '../interfaces/construction-company-create-request';
import { Router } from '@angular/router';
import { ConstructionCompanyService } from '../services/construction-company.service';
import { LoginService } from '../../login/services/login.service';

@Component({
  selector: 'app-construction-company-create',
  templateUrl: './construction-company-create.component.html',
  styleUrl: './construction-company-create.component.css'
})
export class ConstructionCompanyCreateComponent {

  userId: string | undefined = undefined

  constructor(private constructionCompanyService: ConstructionCompanyService, private router: Router, private loginService: LoginService) {

    this.obtainUserId();
    if (this.constructionCompanyToCreate.userCreatorId == '') {
      this.router.navigateByUrl('/login');
    }

  }
  constructionCompanyToCreate: ConstructionCompanyCreateRequest = {

    name: '',
    userCreatorId: this.obtainUserId()
  };

  createConstructionCompany(): void {

    console.log(this.constructionCompanyToCreate)
    this.constructionCompanyService.createConstructionCompany(this.constructionCompanyToCreate)
      .subscribe({
        next: () => {
          this.router.navigateByUrl('construction-companies/list')
        },
        error: (errorMessage) => {
          alert(errorMessage.error)
        }
      })
  }

  obtainUserId(): string {
    var userId: string = '';
    this.loginService.getUser()
      .subscribe({
        next: (Response) => {
          userId = Response ? Response.userId : '';
        }
      })
    return userId;
  }

}
