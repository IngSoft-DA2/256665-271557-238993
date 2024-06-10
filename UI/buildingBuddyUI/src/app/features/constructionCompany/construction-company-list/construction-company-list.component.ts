import { Component, OnInit } from '@angular/core';
import { ConstructionCompany } from '../interfaces/construction-company';
import { Router } from '@angular/router';
import { User } from '../../login/interfaces/user';
import { ConstructionCompanyService } from '../services/construction-company.service';
import { LoginService } from '../../login/services/login.service';

@Component({
  selector: 'app-construction-company-list',
  templateUrl: './construction-company-list.component.html',
  styleUrls: ['./construction-company-list.component.css']
})
export class ConstructionCompanyListComponent implements OnInit {

  userLogged?: User;
  constructionCompanyOfUser: ConstructionCompany | undefined = undefined;


  constructor(private constructionCompanyService: ConstructionCompanyService, private loginService: LoginService, private router: Router) {
    
    this.loginService.getUser()
    .subscribe({
      next: (Response) => {
        this.userLogged = Response;
      },
      error: () => {
        this.router.navigateByUrl('/login');
      }
    })
  }
  ngOnInit(): void {
    if (this.userLogged !== undefined) {
      this.constructionCompanyService.getConstructionCompanyByUserCreatorId(this.userLogged.userId)
        .subscribe({
          next: (Response) => {
            this.constructionCompanyOfUser = Response;
          },
          error:() => {
            
          }
        });
    }
  }

  

  


}
