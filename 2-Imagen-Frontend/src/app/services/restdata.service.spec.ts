import { TestBed } from '@angular/core/testing';

import { RestdataService } from './restdata.service';

describe('RestdataService', () => {
  let service: RestdataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RestdataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
