import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { LoginService } from '../../features/login/services/login.service';

describe('AuthGuard', () => {
  let authGuard: AuthGuard;
  let routerSpy = { navigate: jasmine.createSpy('navigate') };
  let loginServiceSpy: jasmine.SpyObj<LoginService>;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('LoginService', ['getUser']);

    TestBed.configureTestingModule({
      providers: [
        AuthGuard,
        { provide: Router, useValue: routerSpy },
        { provide: LoginService, useValue: spy }
      ]
    });

    authGuard = TestBed.inject(AuthGuard);
    loginServiceSpy = TestBed.inject(LoginService) as jasmine.SpyObj<LoginService>;
  });

  it('should be created', () => {
    expect(authGuard).toBeTruthy();
  });
});