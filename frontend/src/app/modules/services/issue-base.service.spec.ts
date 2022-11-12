import { TestBed } from '@angular/core/testing';

import { IssueBaseService } from './issue-base.service';

describe('IssueBaseService', () => {
  let service: IssueBaseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IssueBaseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
