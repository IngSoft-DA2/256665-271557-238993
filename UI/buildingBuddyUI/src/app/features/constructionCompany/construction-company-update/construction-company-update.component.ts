import { Component } from '@angular/core';
import { ConstructionCompany } from '../interfaces/construction-company';
import { ConstructionCompanyService } from '../services/construction-company.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-construction-company-update',
  templateUrl: './construction-company-update.component.html',
  styleUrl: './construction-company-update.component.css'
})
export class ConstructionCompanyUpdateComponent{
  constructionCompanyToUpd?: ConstructionCompany;

  idOfConstructionCompany: string = '';
  constructionCompanyFound: boolean = false;

  constructor(private constructionCompanyService: ConstructionCompanyService, private route: ActivatedRoute, private router: Router) {

    route.paramMap
    .subscribe({
      next: (params) =>{
        this.idOfConstructionCompany = params.get("id")!;
      }
    })

    if (this.idOfConstructionCompany !== undefined) {
      constructionCompanyService.getConstructionCompanyById(this.idOfConstructionCompany)
        .subscribe({
          next: (Response) => {
            this.constructionCompanyToUpd = Response;
            this.constructionCompanyFound = true;
          },
          error: () => {
            alert("Construction company was not found");
            this.router.navigateByUrl('/construction-companies/list')
          }
        })
    }

  }
  updateChanges(): void {
    if (this.idOfConstructionCompany !== undefined && this.constructionCompanyToUpd) {
      this.constructionCompanyService.updateConstructionCompany(this.idOfConstructionCompany, this.constructionCompanyToUpd)
        .subscribe({
          next: (Response) => {
            alert("Updated with sucess!");
            this.router.navigateByUrl('/construction-companies/list')
          },
          error: (errorMessage) => {
            alert(errorMessage.error);
          }
        });
    }
    else {
      alert("Imposible to update, it is the failing the getter of the construction company");
    }

  }

}
