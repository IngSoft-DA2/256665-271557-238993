import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateMaintenanceRequestComponent } from './create-maintenance-request.component';

describe('CreateMaintenanceRequestComponent', () => {
  let component: CreateMaintenanceRequestComponent;
  let fixture: ComponentFixture<CreateMaintenanceRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateMaintenanceRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateMaintenanceRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
