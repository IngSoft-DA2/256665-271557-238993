import { TestBed } from '@angular/core/testing';

import { RequestHandlerService } from './request-handler.service';

describe('RequestHandlerService', () => {
  let service: RequestHandlerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RequestHandlerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
