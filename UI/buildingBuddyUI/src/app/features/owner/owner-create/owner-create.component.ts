import { Component } from '@angular/core';
import { OwnerService } from '../services/owner.service';
import { OwnerCreateRequest } from '../interfaces/owner-create-request';

@Component({
  selector: 'app-owner-create',
  templateUrl: './owner-create.component.html',
  styleUrl: './owner-create.component.css'
})
export class OwnerCreateComponent {
  ownerToCreate: OwnerCreateRequest = {
    firstname: '',
    lastname: '',
    email: '',
  }

  constructor(private ownerService: OwnerService) {
  }

  createOwner() {

    this.ownerService.createOwner(this.ownerToCreate)
      .subscribe({
        next: () => {
          alert('Owner create with sucess')
          this.resetOwnerValues();
        },
        error: (errorMessage) => {
          alert(errorMessage.error);
        }

      })
  }


  private resetOwnerValues(): void {

    this.ownerToCreate.firstname = '';
    this.ownerToCreate.lastname = '';
    this.ownerToCreate.email = '';
  };
}
