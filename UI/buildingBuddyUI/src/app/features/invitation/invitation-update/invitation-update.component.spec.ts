import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvitationUpdateComponent } from './invitation-update.component';

describe('InvitationUpdateComponent', () => {
  let component: InvitationUpdateComponent;
  let fixture: ComponentFixture<InvitationUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InvitationUpdateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InvitationUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
