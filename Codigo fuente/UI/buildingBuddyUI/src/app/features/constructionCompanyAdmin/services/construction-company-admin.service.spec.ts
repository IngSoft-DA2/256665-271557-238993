import { TestBed } from '@angular/core/testing';

import { ConstructionCompanyAdminService } from './construction-company-admin.service';

describe('ConstructionCompanyAdminService', () => {
  let service: ConstructionCompanyAdminService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConstructionCompanyAdminService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
