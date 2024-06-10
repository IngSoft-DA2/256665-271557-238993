import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConstructionCompanyCreateComponent } from './construction-company-create.component';

describe('ConstructionCompanyCreateComponent', () => {
  let component: ConstructionCompanyCreateComponent;
  let fixture: ComponentFixture<ConstructionCompanyCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConstructionCompanyCreateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConstructionCompanyCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
