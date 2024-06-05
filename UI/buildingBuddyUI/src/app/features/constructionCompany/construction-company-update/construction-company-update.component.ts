import { Component } from '@angular/core';
import { ConstructionCompany } from '../interfaces/construction-company';
import { ConstructionCompanyService } from '../services/construction-company.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-construction-company-update',
  templateUrl: './construction-company-update.component.html',
  styleUrl: './construction-company-update.component.css'
})
export class ConstructionCompanyUpdateComponent {

  constructionCompanyToUpd?: ConstructionCompany;
  idOfConstructionCompany?: string;
  constructionCompanyFound : boolean = false;

  constructor(private constructionCompanyService: ConstructionCompanyService, private route: ActivatedRoute) {

    route.queryParams
      .subscribe({
        next: (queryParams) => {
          this.idOfConstructionCompany = queryParams['id'];
        }
      })
    if (this.idOfConstructionCompany !== undefined) {
      constructionCompanyService.getConstructionCompanyById(this.idOfConstructionCompany)
      .subscribe({
        next: (Response) => {
          this.constructionCompanyToUpd = Response;
          this.constructionCompanyFound = true;
        },
      })
    }
    
  }

  updateChanges() : void 
  {
    this.constructionCompanyService.updateConstructionCompany(this.constructionCompanyToUpd)
    .subscribe({
      next : (Response) => {
        alert("Updated with sucess!");
        this.router.navigateByUrl('/construction-companies/list')
      },
      error : (errorMessage) => {
        alert(errorMessage.error);
      }
    });
  }

}
