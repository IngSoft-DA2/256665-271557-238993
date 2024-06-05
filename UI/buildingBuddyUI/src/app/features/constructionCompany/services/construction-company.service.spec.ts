import { TestBed } from '@angular/core/testing';

import { ConstructionCompanyService } from './construction-company.service';

describe('ConstructionCompanyService', () => {
  let service: ConstructionCompanyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConstructionCompanyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
