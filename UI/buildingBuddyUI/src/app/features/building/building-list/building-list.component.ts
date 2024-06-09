import { Component } from '@angular/core';
import { Building } from '../interfaces/building';
import { BuildingService } from '../services/building.service';
import { Manager } from '../../manager/interfaces/manager';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from '../../login/services/login.service';
import { User } from '../../login/interfaces/user';
import { ConstructionCompany } from '../../constructionCompany/interfaces/construction-company';
import { ConstructionCompanyService } from '../../constructionCompany/services/construction-company.service';

@Component({
  selector: 'app-building-list',
  templateUrl: './building-list.component.html',
  styleUrl: './building-list.component.css'
})
export class BuildingListComponent {

  buildings: Building[] = [];
  hasManager: boolean = false;
  userLogged?: User = undefined;
  constructionCompanyOfUser: ConstructionCompany | undefined = undefined;
  hasConstructionCompany: boolean = true;

  constructor(private buildingService: BuildingService, private constructionCompanyService: ConstructionCompanyService, private loginService: LoginService, private router: Router) {

    this.loginService.getUser()
      .subscribe({
        next: (Response) => {
          this.userLogged = Response
          if (this.userLogged) {
            this.constructionCompanyService.getConstructionCompanyByUserCreatorId(this.userLogged.userId)
              .subscribe({
                next: (Response) => {
                  this.constructionCompanyOfUser = Response;
                  this.hasConstructionCompany = this.constructionCompanyOfUser !== undefined;
                }
              });

            this.buildingService.getAllBuildings(this.userLogged.userId)
              .subscribe({
                next: (Response) => {
                  this.buildings = Response
                  console.log(this.buildings)
                }
              })
          }
          else {
            alert("User was not found, redirecting")
            this.router.navigateByUrl('/login');
          }
        }
      });
    }
    

  checkIfItHasManager(manager?: Manager): string {
    return manager ? manager.name : 'No manager at the moment';
  }


  deleteBuilding(buildingId: string): void {
    this.buildingService.deleteBuilding(buildingId)
      .subscribe({
        next: () => {
          this.buildings = this.buildings.filter(b => b.id !== buildingId);
        },
        error: (errorMessage) => {
          alert("Cannot delete this building, communicate with an admin")
        }
      })
  }
}
