import { Component } from '@angular/core';
import { Building } from '../interfaces/building';
import { BuildingService } from '../services/building.service';
import { Manager } from '../../manager/interfaces/manager';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from '../../login/services/login.service';
import { User } from '../../login/interfaces/user';
import { ConstructionCompany } from '../../constructionCompany/interfaces/construction-company';
import { ConstructionCompanyService } from '../../constructionCompany/services/construction-company.service';
import { SystemUserRoleEnum } from '../../invitation/interfaces/enums/system-user-role-enum';
import { ManagerService } from '../../manager/services/manager.service';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-building-list',
  templateUrl: './building-list.component.html',
  styleUrls: ['./building-list.component.css']
})
export class BuildingListComponent {

  buildings: Building[] = [];
  hasManager: boolean = false;
  userLogged?: User = undefined;
  constructionCompanyOfUser: ConstructionCompany | undefined = undefined;
  hasConstructionCompany: boolean = true;
  SystemUserRoleEnumValues = SystemUserRoleEnum;
  isDisplayed: boolean = false;
  managerId: string = "";

  constructor(
    private buildingService: BuildingService,
    private constructionCompanyService: ConstructionCompanyService,
    private loginService: LoginService,
    private router: Router,
    private managerService: ManagerService,
    private route: ActivatedRoute
  ) {

    this.route.queryParams.subscribe(params => {
      this.managerId = params['managerId'];
    });

    this.loginService.getUser()
      .subscribe({
        next: (response) => {
          this.userLogged = response;

          if (this.userLogged) {
            if (this.userLogged.userRole === SystemUserRoleEnum.ConstructionCompanyAdmin) {
              this.constructionCompanyService.getConstructionCompanyByUserCreatorId(this.userLogged.userId)
                .subscribe({
                  next: (response) => {
                    this.constructionCompanyOfUser = response;
                    this.hasConstructionCompany = this.constructionCompanyOfUser !== undefined;
                  }
                });

              this.buildingService.getAllBuildings(this.userLogged.userId)
                .subscribe({
                  next: (response) => {
                    this.buildings = response;
                  }
                });
            } 
            else {
              if(this.managerId == ""){
                this.managerService.getManagerById(this.userLogged?.userId)
                  .subscribe({
                    next: (Response) => {
                      Response.buildingsId.forEach(buildingId => {
                        this.buildingService.getBuildingById(buildingId)
                          .subscribe({
                            next: (Response) => {
                              console.log(Response);
                              this.buildings.push(Response);
                            }
                          })
                      });
                    },
                    error: (errorMessage) => {
                      console.log(errorMessage.error);
                    }
                  });
              }
              else{
                this.managerService.getManagerById(this.managerId)
                  .subscribe({
                    next: (Response) => {
                      Response.buildingsId.forEach(buildingId => {
                        this.buildingService.getBuildingById(buildingId)
                          .subscribe({
                            next: (Response) => {
                              console.log(Response);
                              this.buildings.push(Response);
                            }
                          })
                      });
                    },
                    error: (errorMessage) => {
                      console.log(errorMessage.error);
                    }
                  });
              }
          }
          } else {
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
        error: () => {
          alert("Cannot delete this building, communicate with an admin");
        }
      });
  }

  getOwnersByManagerRouteOnAdmin(): void {
    const queryParams = new HttpParams()
    .set('managerId', this.managerId);
    alert(`owners/list?${queryParams}`);

  }
}
