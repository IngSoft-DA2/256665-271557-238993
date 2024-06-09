import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportMaintenanceRequestsByBuildingComponent } from './report-maintenance-requests-by-building.component';

describe('ReportMaintenanceRequestsByBuildingComponent', () => {
  let component: ReportMaintenanceRequestsByBuildingComponent;
  let fixture: ComponentFixture<ReportMaintenanceRequestsByBuildingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ReportMaintenanceRequestsByBuildingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReportMaintenanceRequestsByBuildingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
