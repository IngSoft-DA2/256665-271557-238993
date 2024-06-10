import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HTTP_INTERCEPTORS, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import ForbiddenHandlerInterceptor from './forbidden-handler.interceptor';

describe('ForbiddenHandlerInterceptor', () => {
  let httpMock: HttpTestingController;
  let httpClient: HttpClient;
  let routerSpy = { navigate: jasmine.createSpy('navigate') };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        { provide: HTTP_INTERCEPTORS, useClass: ForbiddenHandlerInterceptor, multi: true },
        { provide: Router, useValue: routerSpy }
      ]
    });

    httpMock = TestBed.inject(HttpTestingController);
    httpClient = TestBed.inject(HttpClient);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    const interceptor = TestBed.inject(ForbiddenHandlerInterceptor);
    expect(interceptor).toBeTruthy();
  });

  it('should navigate to /login on 403 error', () => {
    httpClient.get('/test').subscribe(
      response => fail('should have failed with the 403 error'),
      (error: HttpErrorResponse) => {
        expect(error.status).toEqual(403);
      }
    );

    const req = httpMock.expectOne('/test');
    req.flush('Forbidden', { status: 403, statusText: 'Forbidden' });

    expect(routerSpy.navigate).toHaveBeenCalledWith(['/login']);
  });
});
