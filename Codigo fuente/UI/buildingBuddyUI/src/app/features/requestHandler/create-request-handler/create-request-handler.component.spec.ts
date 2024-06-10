import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateRequestHandlerComponent } from './create-request-handler.component';

describe('CreateRequestHandlerComponent', () => {
  let component: CreateRequestHandlerComponent;
  let fixture: ComponentFixture<CreateRequestHandlerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateRequestHandlerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateRequestHandlerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
