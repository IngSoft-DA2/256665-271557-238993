import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuildingUpdateComponent } from './building-update.component';

describe('BuildingUpdateComponent', () => {
  let component: BuildingUpdateComponent;
  let fixture: ComponentFixture<BuildingUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BuildingUpdateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BuildingUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
