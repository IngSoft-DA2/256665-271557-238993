import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReportMaintenanceRequestsByBuildingComponent } from './features/Reports/report-maintenance-requests-by-building/report-maintenance-requests-by-building.component';
import { NavbarComponent } from './core/navbar/navbar.component';
import { InvitationListComponent } from './features/invitation/invitation-list/invitation-list.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { InvitationUpdateComponent } from './features/invitation/invitation-update/invitation-update.component';
import { InvitationCreateComponent } from './features/invitation/invitation-create/invitation-create.component';
import { InvitationListByEmailComponent } from './features/invitation/invitation-list-by-email/invitation-list-by-email.component';
import { LandingPageComponent } from './features/landingPage/landing-page/landing-page.component';
import { AdminCreateComponent } from './features/administrator/admin-create/admin-create.component';
import { ReportsListComponent } from './features/Reports/reports-list/reports-list.component';
import { CategoryCreateComponent } from './features/category/category-create/category-create.component';
import { CategoryTreeComponent } from './features/category/category-tree/category-tree.component';
import { ReportMaintenanceReqByReqHandlerComponent } from './features/Reports/report-maintenance-req-by-req-handler/report-maintenance-req-by-req-handler.component';
import { ReportMaintenanceReqByCategoryComponent } from './features/Reports/report-maintenance-req-by-category/report-maintenance-req-by-category.component';
import { ReportMaintenanceRequestsByFlatComponent } from './features/Reports/report-maintenance-requests-by-flat/report-maintenance-requests-by-flat.component';
import { LoginComponent } from './features/login/login.component';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { ErrorInterceptor } from './core/interceptors/forbidden-handler.interceptor';
import { HomeComponent } from './core/home/home.component';
import { CreateMaintenanceRequestComponent } from './features/MaintenanceRequest/create-maintenance-request/create-maintenance-request.component';
import { MaintenanceRequestsListComponent } from './features/MaintenanceRequest/maintenance-requests-list/maintenance-requests-list.component';
import { AssignMaintenanceRequestComponent } from './features/MaintenanceRequest/assign-maintenance-request/assign-maintenance-request.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ReportMaintenanceRequestsByBuildingComponent,
    InvitationListComponent,
    InvitationUpdateComponent,
    InvitationCreateComponent,
    InvitationListByEmailComponent,
    LandingPageComponent,
    AdminCreateComponent,
    ReportsListComponent,
    CategoryCreateComponent,
    CategoryTreeComponent,
    ReportMaintenanceReqByReqHandlerComponent,
    ReportMaintenanceReqByCategoryComponent,
    ReportMaintenanceRequestsByFlatComponent,
    LoginComponent,
    HomeComponent,
    CreateMaintenanceRequestComponent,
    MaintenanceRequestsListComponent,
    AssignMaintenanceRequestComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
