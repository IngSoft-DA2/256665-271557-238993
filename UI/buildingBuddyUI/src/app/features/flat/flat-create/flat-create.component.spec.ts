import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlatCreateComponent } from './flat-create.component';

describe('FlatCreateComponent', () => {
  let component: FlatCreateComponent;
  let fixture: ComponentFixture<FlatCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FlatCreateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FlatCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
