import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuildingListComponent } from './building-list.component';

describe('BuildingListComponent', () => {
  let component: BuildingListComponent;
  let fixture: ComponentFixture<BuildingListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BuildingListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BuildingListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
