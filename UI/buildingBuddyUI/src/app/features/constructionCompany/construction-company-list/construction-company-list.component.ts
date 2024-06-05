import { Component } from '@angular/core';
import { ConstructionCompany } from '../interfaces/construction-company';
import { Router } from '@angular/router';

@Component({
  selector: 'app-construction-company-list',
  templateUrl: './construction-company-list.component.html',
  styleUrl: './construction-company-list.component.css'
})
export class ConstructionCompanyListComponent 
{

  constructionCompany : ConstructionCompany = {
    id : '',
    name: '',
    userCreatorId : '',
    buildingsId: []
  }

  constructor(private router : Router)
  {

  }
  checkIfItHasBuildings() : void
  {
    if(this.constructionCompany.buildingsId[0] !== undefined)
    {
      this.router.navigateByUrl('buildings/list')
    }
    else
    {
      alert("No buildings at the moment")
    }
  }

}
