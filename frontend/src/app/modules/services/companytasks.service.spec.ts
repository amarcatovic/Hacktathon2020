/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CompanytasksService } from './companytasks.service';

describe('Service: Companytasks', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CompanytasksService]
    });
  });

  it('should ...', inject([CompanytasksService], (service: CompanytasksService) => {
    expect(service).toBeTruthy();
  }));
});
