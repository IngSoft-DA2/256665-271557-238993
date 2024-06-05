import { Component } from '@angular/core';
import { ConstructionCompanyCreateRequest } from '../interfaces/construction-company-create-request';
import { Router } from '@angular/router';
import { ConstructionCompanyService } from '../services/construction-company.service';

@Component({
  selector: 'app-construction-company-create',
  templateUrl: './construction-company-create.component.html',
  styleUrl: './construction-company-create.component.css'
})
export class ConstructionCompanyCreateComponent 
{

  constructor(private constructionCompanyServices : ConstructionCompanyService ,private router : Router)
  {

  }
  constructionCompanyToCreate : ConstructionCompanyCreateRequest = {
    
    name: '',
    userCreatorId : ''
  };

}
