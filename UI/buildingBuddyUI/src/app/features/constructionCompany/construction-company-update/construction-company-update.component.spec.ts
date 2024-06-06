import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConstructionCompanyUpdateComponent } from './construction-company-update.component';

describe('ConstructionCompanyUpdateComponent', () => {
  let component: ConstructionCompanyUpdateComponent;
  let fixture: ComponentFixture<ConstructionCompanyUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConstructionCompanyUpdateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConstructionCompanyUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
