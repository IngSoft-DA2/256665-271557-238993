import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaintenanceRequestListByReqHandlerComponent } from './maintenance-request-list-by-req-handler.component';

describe('MaintenanceRequestListByReqHandlerComponent', () => {
  let component: MaintenanceRequestListByReqHandlerComponent;
  let fixture: ComponentFixture<MaintenanceRequestListByReqHandlerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MaintenanceRequestListByReqHandlerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MaintenanceRequestListByReqHandlerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
