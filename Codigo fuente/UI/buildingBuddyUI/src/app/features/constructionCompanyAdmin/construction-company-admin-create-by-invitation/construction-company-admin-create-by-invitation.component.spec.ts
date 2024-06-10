import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ConstructionCompanyAdminCreateByInvitationComponent } from './construction-company-admin-create-by-invitation.component';

describe('ConstructionCompanyAdminCreateByInvitationComponent', () => {
  let component: ConstructionCompanyAdminCreateByInvitationComponent;
  let fixture: ComponentFixture<ConstructionCompanyAdminCreateByInvitationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConstructionCompanyAdminCreateByInvitationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConstructionCompanyAdminCreateByInvitationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
