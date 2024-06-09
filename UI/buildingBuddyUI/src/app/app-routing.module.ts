import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InvitationListComponent } from './features/invitation/invitation-list/invitation-list.component';
import { InvitationUpdateComponent } from './features/invitation/invitation-update/invitation-update.component';
import { InvitationCreateComponent } from './features/invitation/invitation-create/invitation-create.component';
import { InvitationListByEmailComponent } from './features/invitation/invitation-list-by-email/invitation-list-by-email.component';
import { ManagerCreateComponent } from './features/manager/features/manager-create/manager-create.component';
import { LandingPageComponent } from './features/landingPage/landing-page/landing-page.component';
import { AdminCreateComponent } from './features/administrator/admin-create/admin-create.component';
import { ConstructionCompanyAdminCreateByInvitationComponent } from './features/constructionCompanyAdmin/construction-company-admin-create-by-invitation/construction-company-admin-create-by-invitation.component';
import { ManagerListComponent } from './features/manager/features/manager-list/manager-list.component';
import { ConstructionCompanyCreateComponent } from './features/constructionCompany/construction-company-create/construction-company-create.component';
import { ConstructionCompanyListComponent } from './features/constructionCompany/construction-company-list/construction-company-list.component';
import { BuildingListComponent } from './features/building/building-list/building-list.component';
import { ConstructionCompanyUpdateComponent } from './features/constructionCompany/construction-company-update/construction-company-update.component';
import { CategoryCreateComponent } from './features/category/category-create/category-create.component';
import { LoginComponent } from './features/login/login.component';
import { HomeComponent } from './core/home/home.component';
import { CreateMaintenanceRequestComponent } from './features/MaintenanceRequest/create-maintenance-request/create-maintenance-request.component';
import { MaintenanceRequestsListComponent } from './features/MaintenanceRequest/maintenance-requests-list/maintenance-requests-list.component';
import { AssignMaintenanceRequestComponent } from './features/MaintenanceRequest/assign-maintenance-request/assign-maintenance-request.component';
import { CreateRequestHandlerComponent } from './features/requestHandler/create-request-handler/create-request-handler.component';
import { MaintenanceRequestListByReqHandlerComponent } from './features/MaintenanceRequest/maintenance-request-list-by-req-handler/maintenance-request-list-by-req-handler.component';
import { CompleteMaintenanceRequestComponent } from './features/MaintenanceRequest/complete-maintenance-request/complete-maintenance-request.component';
import { BuildingUpdateComponent } from './features/building/building-update/building-update.component';
import { BuildingCreateComponent } from './features/building/building-create/building-create.component';
import { ReportsListComponent } from './features/Reports/reports-list/reports-list.component';
import { ReportMaintenanceRequestsByBuildingComponent } from './features/Reports/report-maintenance-requests-by-building/report-maintenance-requests-by-building.component';
import { ReportMaintenanceReqByReqHandlerComponent } from './features/Reports/report-maintenance-req-by-req-handler/report-maintenance-req-by-req-handler.component';
import { ReportMaintenanceReqByCategoryComponent } from './features/Reports/report-maintenance-req-by-category/report-maintenance-req-by-category.component';
import { ReportMaintenanceRequestsByFlatComponent } from './features/Reports/report-maintenance-requests-by-flat/report-maintenance-requests-by-flat.component';


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
    path: 'managers/create',
    component : ManagerCreateComponent
  },
  {
    path: 'constructionCompanyAdmin/create',
    component : ConstructionCompanyAdminCreateByInvitationComponent
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
    path: 'maintenance-requests/list-by-request-handler',
    component : MaintenanceRequestListByReqHandlerComponent
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
    path: 'maintenance-requests/complete',
    component : CompleteMaintenanceRequestComponent
  },
  {
    path: 'request-handler/create',
    component : CreateRequestHandlerComponent
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
    path: 'managers/list',
    component : ManagerListComponent
  },
  {
    path : 'construction-companies/create',
    component : ConstructionCompanyCreateComponent
  },
  {
    path: 'construction-companies/list',
    component : ConstructionCompanyListComponent
  },
  {
    path: 'construction-companies/:id/update',
    component : ConstructionCompanyUpdateComponent
  },
  {
    path: 'buildings/:constructionCompanyId/create',
    component : BuildingCreateComponent
  },
  {
    path : 'buildings/list',
    component : BuildingListComponent
  },
  {
    path : 'buildings/:buildingId/update',
    component : BuildingUpdateComponent
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
  },

  { path: '**', 
    redirectTo: 'login'
  }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule 
{ 
}
