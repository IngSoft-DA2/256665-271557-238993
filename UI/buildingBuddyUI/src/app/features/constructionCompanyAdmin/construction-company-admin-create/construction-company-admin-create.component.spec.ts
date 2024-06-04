import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConstructionCompanyAdminCreateComponent } from './construction-company-admin-create.component';

describe('ConstructionCompanyAdminCreateComponent', () => {
  let component: ConstructionCompanyAdminCreateComponent;
  let fixture: ComponentFixture<ConstructionCompanyAdminCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConstructionCompanyAdminCreateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConstructionCompanyAdminCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
