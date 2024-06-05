import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConstructionCompanyListComponent } from './construction-company-list.component';

describe('ConstructionCompanyListComponent', () => {
  let component: ConstructionCompanyListComponent;
  let fixture: ComponentFixture<ConstructionCompanyListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConstructionCompanyListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConstructionCompanyListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
