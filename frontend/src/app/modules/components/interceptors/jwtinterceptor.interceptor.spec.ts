import { TestBed } from '@angular/core/testing';

import { JwtinterceptorInterceptor } from './jwtinterceptor.interceptor';

describe('JwtinterceptorInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      JwtinterceptorInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: JwtinterceptorInterceptor = TestBed.inject(JwtinterceptorInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
