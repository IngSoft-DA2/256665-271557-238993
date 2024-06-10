import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompleteMaintenanceRequestComponent } from './complete-maintenance-request.component';

describe('CompleteMaintenanceRequestComponent', () => {
  let component: CompleteMaintenanceRequestComponent;
  let fixture: ComponentFixture<CompleteMaintenanceRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CompleteMaintenanceRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CompleteMaintenanceRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
