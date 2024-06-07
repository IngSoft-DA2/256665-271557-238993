import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-maintenance-requests-list',
  templateUrl: './maintenance-requests-list.component.html',
  styleUrl: './maintenance-requests-list.component.css'
})
export class MaintenanceRequestsListComponent {

  constructor(private router: Router) { }

  goToMaintenanceRequestCreationForm(): void {
    this.router.navigateByUrl('maintenance-requests/create');
  }
}
