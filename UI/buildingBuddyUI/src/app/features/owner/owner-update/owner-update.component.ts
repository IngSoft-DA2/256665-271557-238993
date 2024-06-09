import { Component } from '@angular/core';
import { Owner } from '../interfaces/owner';
import { OwnerUpdateRequest } from '../interfaces/owner-update-request';
import { OwnerService } from '../services/owner.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SystemUserRoleEnum } from '../../invitation/interfaces/enums/system-user-role-enum';

@Component({
  selector: 'app-owner-update',
  templateUrl: './owner-update.component.html',
  styleUrl: './owner-update.component.css'
})
export class OwnerUpdateComponent {
  ownerId?: string;
  buildingId?: string;
  ownerToUpdate: OwnerUpdateRequest = {
    firstname: '',
    lastname: '',
    email: ''
  };

  constructor(private ownerService: OwnerService, private router: Router, private route: ActivatedRoute) {

    this.obtainBuildinIdFromQuery();

    this.obtaindOwnerIdFromRoute();
    if (this.ownerId) {
      this.ownerService.getOwnerById(this.ownerId)
        .subscribe({
          next: (Response) => {
            this.ownerToUpdate = Response;
          },
          error: (errorMessage) => {
            alert(errorMessage.error);
            this.router.navigateByUrl(`/buildings/${this.buildingId}/owners`)
          }
        });
    }
    else {
      alert("Owner id was not found, redirecting");
      this.router.navigateByUrl(`/buildings/${this.buildingId}/owners`)
    }

  }
  updateOwner(): void {
    if (this.ownerId) {
      this.ownerService.updateOwner(this.ownerId, this.ownerToUpdate)
        .subscribe({
          next: () => {
            alert("Owner was updated successfully");
            this.router.navigateByUrl(`/buildings/${this.buildingId}/owners`)
          }
        })
    }
    else {
      alert("Owner id was not found, redirecting");
      this.router.navigateByUrl(`/buildings/${this.buildingId}/owners`)
    }

  }


  private obtainBuildinIdFromQuery() {

    this.route.queryParams.subscribe(params => {
      this.buildingId = params['buildingId'];
    });
  }

  private obtaindOwnerIdFromRoute() {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.ownerId = params.get('ownerId') ?? undefined;
        if (this.ownerId === undefined) {
          alert("Owner was not found, redirecting");
          this.router.navigateByUrl('/home') //Same question as owner list
        }
      }
    })
  }


}
