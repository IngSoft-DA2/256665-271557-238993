import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportMaintenanceRequestsByFlatComponent } from './report-maintenance-requests-by-flat.component';

describe('ReportMaintenanceRequestsByFlatComponent', () => {
  let component: ReportMaintenanceRequestsByFlatComponent;
  let fixture: ComponentFixture<ReportMaintenanceRequestsByFlatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ReportMaintenanceRequestsByFlatComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReportMaintenanceRequestsByFlatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
