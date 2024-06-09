import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportMaintenanceRequestsByBuildingComponent } from './features/Reports/report-maintenance-requests-by-building/report-maintenance-requests-by-building.component';
import { InvitationListComponent } from './features/invitation/invitation-list/invitation-list.component';
import { InvitationUpdateComponent } from './features/invitation/invitation-update/invitation-update.component';
import { InvitationCreateComponent } from './features/invitation/invitation-create/invitation-create.component';
import { InvitationListByEmailComponent } from './features/invitation/invitation-list-by-email/invitation-list-by-email.component';
import { LandingPageComponent } from './features/landingPage/landing-page/landing-page.component';
import { AdminCreateComponent } from './features/administrator/admin-create/admin-create.component';
import { ReportsListComponent } from './features/Reports/reports-list/reports-list.component';
import { CategoryCreateComponent } from './features/category/category-create/category-create.component';
import { ReportMaintenanceReqByReqHandlerComponent } from './features/Reports/report-maintenance-req-by-req-handler/report-maintenance-req-by-req-handler.component';
import { ReportMaintenanceReqByCategoryComponent } from './features/Reports/report-maintenance-req-by-category/report-maintenance-req-by-category.component';
import { ReportMaintenanceRequestsByFlatComponent } from './features/Reports/report-maintenance-requests-by-flat/report-maintenance-requests-by-flat.component';
import { LoginComponent } from './features/login/login.component';
import { HomeComponent } from './core/home/home.component';
import { CreateMaintenanceRequestComponent } from './features/MaintenanceRequest/create-maintenance-request/create-maintenance-request.component';
import { MaintenanceRequestsListComponent } from './features/MaintenanceRequest/maintenance-requests-list/maintenance-requests-list.component';
import { AssignMaintenanceRequestComponent } from './features/MaintenanceRequest/assign-maintenance-request/assign-maintenance-request.component';


const routes: Routes =
[

  {
    path: 'invitations/list',
    component: InvitationListComponent
  },

  {
    path: 'invitations/update/:id',
    component: InvitationUpdateComponent
  },
  {
    path : 'invitations/create',
    component : InvitationCreateComponent
  },
  {
    path: 'invitations/guests/list',
    component : InvitationListByEmailComponent
  },
  {
    path: '',
    component : LandingPageComponent
  },
  {
    path: 'admins/create',
    component : AdminCreateComponent
  },
  {
    path: 'maintenance-requests/list',
    component : MaintenanceRequestsListComponent
  },
  {
    path: 'maintenance-requests/create',
    component : CreateMaintenanceRequestComponent
  },
  {
    path: 'maintenance-requests/assign',
    component : AssignMaintenanceRequestComponent
  },
  {
  path: 'reports/list',
  component: ReportsListComponent
  },
  {
  path: 'reports/requests-by-building',
  component: ReportMaintenanceRequestsByBuildingComponent
  },
  {
    path: 'reports/requests-by-request-handler',
    component: ReportMaintenanceReqByReqHandlerComponent
  },
  {
    path: 'reports/requests-by-category',
    component: ReportMaintenanceReqByCategoryComponent
  },
  {
    path: 'reports/requests-by-flat',
    component: ReportMaintenanceRequestsByFlatComponent
  },
  {
    path: 'categories/create',
    component : CategoryCreateComponent
  },
  {
    path: 'login',
    component : LoginComponent
  },
  {
    path: 'home',
    component : HomeComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule 
{ 
}
