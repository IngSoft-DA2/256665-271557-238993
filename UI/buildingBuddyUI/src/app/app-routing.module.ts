import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportMaintenanceRequestsByBuildingComponent } from './features/Reports/report-maintenance-requests-by-building/report-maintenance-requests-by-building.component';

const routes: Routes = [
{
  path: '/buildings/maintenance-requests/reports',
  component: ReportMaintenanceRequestsByBuildingComponent
}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule 
{ 
}
