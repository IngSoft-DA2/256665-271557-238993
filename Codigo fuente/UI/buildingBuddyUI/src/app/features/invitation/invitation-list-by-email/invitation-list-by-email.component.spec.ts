import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvitationListByEmailComponent } from './invitation-list-by-email.component';

describe('InvitationListByEmailComponent', () => {
  let component: InvitationListByEmailComponent;
  let fixture: ComponentFixture<InvitationListByEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InvitationListByEmailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InvitationListByEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
