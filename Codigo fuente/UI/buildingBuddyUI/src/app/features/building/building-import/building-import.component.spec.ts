import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuildingImportComponent } from './building-import.component';

describe('BuildingImportComponent', () => {
  let component: BuildingImportComponent;
  let fixture: ComponentFixture<BuildingImportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BuildingImportComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BuildingImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
