import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportMaintenanceReqByReqHandlerComponent } from './report-maintenance-req-by-req-handler.component';

describe('ReportMaintenanceReqByReqHandlerComponent', () => {
  let component: ReportMaintenanceReqByReqHandlerComponent;
  let fixture: ComponentFixture<ReportMaintenanceReqByReqHandlerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ReportMaintenanceReqByReqHandlerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReportMaintenanceReqByReqHandlerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
