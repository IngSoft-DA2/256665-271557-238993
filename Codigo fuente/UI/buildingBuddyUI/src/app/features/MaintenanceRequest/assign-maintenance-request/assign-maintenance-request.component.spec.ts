import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignMaintenanceRequestComponent } from './assign-maintenance-request.component';

describe('AssignMaintenanceRequestComponent', () => {
  let component: AssignMaintenanceRequestComponent;
  let fixture: ComponentFixture<AssignMaintenanceRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssignMaintenanceRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AssignMaintenanceRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
