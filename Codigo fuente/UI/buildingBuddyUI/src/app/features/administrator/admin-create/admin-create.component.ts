import { Component } from '@angular/core';
import { AdminCreateRequest } from '../interfaces/admin-create-request';
import { Router } from '@angular/router';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-admin-create',
  templateUrl: './admin-create.component.html',
  styleUrl: './admin-create.component.css'
})
export class AdminCreateComponent 
{

  adminToCreate : AdminCreateRequest =
  {
    firstname : '',
    lastname : '',
    email : '',
    password : ''
  };

  constructor(private adminService : AdminService, private router : Router) {}

  createAdministrator()
  {
    this.adminService.createAdministrator(this.adminToCreate)
    .subscribe({
      next: () => {
        alert("Admin created with success");
        this.router.navigateByUrl('home');
      },
      error: (errorMessage) => {
        alert(errorMessage.error);
      }
    })
  }

}
