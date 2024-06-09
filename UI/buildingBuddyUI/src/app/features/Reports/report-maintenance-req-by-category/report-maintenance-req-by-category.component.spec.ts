import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportMaintenanceReqByCategoryComponent } from './report-maintenance-req-by-category.component';

describe('ReportMaintenanceReqByCategoryComponent', () => {
  let component: ReportMaintenanceReqByCategoryComponent;
  let fixture: ComponentFixture<ReportMaintenanceReqByCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ReportMaintenanceReqByCategoryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReportMaintenanceReqByCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
