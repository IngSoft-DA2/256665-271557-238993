import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuildingCreateComponent } from './building-create.component';

describe('BuildingCreateComponent', () => {
  let component: BuildingCreateComponent;
  let fixture: ComponentFixture<BuildingCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BuildingCreateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BuildingCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
