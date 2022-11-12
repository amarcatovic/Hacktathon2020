import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from "@angular/common/http";
import { Observable } from "rxjs";
import { AuthService } from "../../services/auth.service";

@Injectable()
export class JwtinterceptorInterceptor implements HttpInterceptor {
  constructor(private user: AuthService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    var currentUser = this.user.currentUserValue;
    if (currentUser && currentUser.token.accessToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token.accessToken}`,
        },
      });
    }
    return next.handle(request);
  }
}
