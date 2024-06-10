import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerUpdateComponent } from './owner-update.component';

describe('OwnerUpdateComponent', () => {
  let component: OwnerUpdateComponent;
  let fixture: ComponentFixture<OwnerUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OwnerUpdateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OwnerUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
