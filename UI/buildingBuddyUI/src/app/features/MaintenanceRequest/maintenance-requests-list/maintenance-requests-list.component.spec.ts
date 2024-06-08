import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaintenanceRequestsListComponent } from './maintenance-requests-list.component';

describe('MaintenanceRequestsListComponent', () => {
  let component: MaintenanceRequestsListComponent;
  let fixture: ComponentFixture<MaintenanceRequestsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MaintenanceRequestsListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MaintenanceRequestsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
