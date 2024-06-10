import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { HomeComponent } from "./core/home/home.component";
import { AuthInterceptor } from "./core/interceptors/auth.interceptor";
import { NavbarComponent } from "./core/navbar/navbar.component";
import { AssignMaintenanceRequestComponent } from "./features/MaintenanceRequest/assign-maintenance-request/assign-maintenance-request.component";
import { CompleteMaintenanceRequestComponent } from "./features/MaintenanceRequest/complete-maintenance-request/complete-maintenance-request.component";
import { CreateMaintenanceRequestComponent } from "./features/MaintenanceRequest/create-maintenance-request/create-maintenance-request.component";
import { MaintenanceRequestListByReqHandlerComponent } from "./features/MaintenanceRequest/maintenance-request-list-by-req-handler/maintenance-request-list-by-req-handler.component";
import { MaintenanceRequestsListComponent } from "./features/MaintenanceRequest/maintenance-requests-list/maintenance-requests-list.component";
import { AdminCreateComponent } from "./features/administrator/admin-create/admin-create.component";
import { BuildingCreateComponent } from "./features/building/building-create/building-create.component";
import { BuildingListComponent } from "./features/building/building-list/building-list.component";
import { BuildingUpdateComponent } from "./features/building/building-update/building-update.component";
import { CategoryCreateComponent } from "./features/category/category-create/category-create.component";
import { CategoryTreeComponent } from "./features/category/category-tree/category-tree.component";
import { ConstructionCompanyCreateComponent } from "./features/constructionCompany/construction-company-create/construction-company-create.component";
import { ConstructionCompanyListComponent } from "./features/constructionCompany/construction-company-list/construction-company-list.component";
import { ConstructionCompanyUpdateComponent } from "./features/constructionCompany/construction-company-update/construction-company-update.component";
import { ConstructionCompanyAdminCreateByInvitationComponent } from "./features/constructionCompanyAdmin/construction-company-admin-create-by-invitation/construction-company-admin-create-by-invitation.component";
import { InvitationCreateComponent } from "./features/invitation/invitation-create/invitation-create.component";
import { InvitationListByEmailComponent } from "./features/invitation/invitation-list-by-email/invitation-list-by-email.component";
import { InvitationListComponent } from "./features/invitation/invitation-list/invitation-list.component";
import { InvitationUpdateComponent } from "./features/invitation/invitation-update/invitation-update.component";
import { LandingPageComponent } from "./features/landingPage/landing-page/landing-page.component";
import { LoginComponent } from "./features/login/login.component";
import { ManagerCreateComponent } from "./features/manager/features/manager-create/manager-create.component";
import { ManagerListComponent } from "./features/manager/features/manager-list/manager-list.component";
import { OwnerCreateComponent } from "./features/owner/owner-create/owner-create.component";
import { CreateRequestHandlerComponent } from "./features/requestHandler/create-request-handler/create-request-handler.component";
import { ReportsListComponent } from "./features/Reports/reports-list/reports-list.component";
import { ReportMaintenanceRequestsByBuildingComponent } from "./features/Reports/report-maintenance-requests-by-building/report-maintenance-requests-by-building.component";
import { ReportMaintenanceReqByReqHandlerComponent } from "./features/Reports/report-maintenance-req-by-req-handler/report-maintenance-req-by-req-handler.component";
import { ReportMaintenanceReqByCategoryComponent } from "./features/Reports/report-maintenance-req-by-category/report-maintenance-req-by-category.component";
import { ReportMaintenanceRequestsByFlatComponent } from "./features/Reports/report-maintenance-requests-by-flat/report-maintenance-requests-by-flat.component";
import { BuildingImportComponent } from './features/building/building-import/building-import.component';
import { OwnerListComponent } from './features/owner/owner-list/owner-list.component';
import { OwnerUpdateComponent } from './features/owner/owner-update/owner-update.component';
import ForbiddenHandlerInterceptor from "./core/interceptors/forbidden-handler.interceptor";
import { FlatCreateComponent } from "./features/flat/flat-create/flat-create.component";

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    InvitationListComponent,
    InvitationUpdateComponent,
    InvitationCreateComponent,
    InvitationListByEmailComponent,
    ManagerCreateComponent,
    ConstructionCompanyAdminCreateByInvitationComponent,
    LandingPageComponent,
    AdminCreateComponent,
    ManagerListComponent,
    ConstructionCompanyCreateComponent,
    ConstructionCompanyListComponent,
    BuildingListComponent,
    ConstructionCompanyUpdateComponent,
    CategoryCreateComponent,
    CategoryTreeComponent,
    LoginComponent,
    HomeComponent,
    CreateMaintenanceRequestComponent,
    MaintenanceRequestsListComponent,
    AssignMaintenanceRequestComponent,
    CreateRequestHandlerComponent,
    MaintenanceRequestListByReqHandlerComponent,
    CompleteMaintenanceRequestComponent,
    BuildingUpdateComponent,
    BuildingCreateComponent,
    OwnerCreateComponent,
    ReportsListComponent,
    ReportMaintenanceRequestsByBuildingComponent,
    ReportMaintenanceReqByReqHandlerComponent,
    ReportMaintenanceReqByCategoryComponent,
    ReportMaintenanceRequestsByFlatComponent,
    BuildingImportComponent,
    OwnerListComponent,
    OwnerUpdateComponent,
    FlatCreateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ForbiddenHandlerInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
