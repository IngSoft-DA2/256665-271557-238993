import { Component } from '@angular/core';
import { Owner } from '../interfaces/owner';
import { OwnerUpdateRequest } from '../interfaces/owner-update-request';
import { OwnerService } from '../services/owner.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-owner-update',
  templateUrl: './owner-update.component.html',
  styleUrl: './owner-update.component.css'
})
export class OwnerUpdateComponent 
{
  ownerId? : string;

  ownerToUpdate : OwnerUpdateRequest ={
    firstname : '',
    lastname : '',
    email : ''
  };

  constructor(private ownerService : OwnerService, private router : Router, private route : ActivatedRoute)
  {
    this.obtaindOwnerIdFromRoute();
    if(this.ownerId)
    {
      this.ownerService.getOwnerById(this.ownerId)
      .subscribe({
        next: (Response) => {
          this.ownerToUpdate = Response;
        },
        error: (errorMessage) => {
          alert(errorMessage.error);
          this.router.navigateByUrl('/home') //Same question as owner list
        }
      });
    }
    else{
      alert("Owner id was not found, redirecting");
      this.router.navigateByUrl('/home') //Same question as owner list
    }
    
  }


  updateOwner() : void {
    if(this.ownerId)
    {
      this.ownerService.updateOwner(this.ownerId, this.ownerToUpdate)
      .subscribe({
        next: () => {
          alert("Owner was updated successfully");
        }
      })
    }
    else{
      alert("Owner id was not found, redirecting");
      this.router.navigateByUrl('/home') //Same question as owner list
    }
    
  }

  private obtaindOwnerIdFromRoute()
  {
    this.route.paramMap.subscribe({
      next:(params) => {
        this.ownerId = params.get('ownerId') ?? undefined;
        if(this.ownerId === undefined){
          alert("Owner was not found, redirecting");
          this.router.navigateByUrl('/home') //Same question as owner list
        }
      }
    })
  }

}
