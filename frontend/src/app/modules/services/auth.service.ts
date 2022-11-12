import { Injectable } from "@angular/core";
import { CookieService } from "ngx-cookie-service";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { environment } from "environments/environment.prod";
@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(private http: HttpClient, private cookieService: CookieService) {}

  registration(
    email: string,
    password: string,
    name: string,
    photoUrl?: string
  ) {
    console.log(email);
    console.log(password);
    console.log(name);
    return this.http.post(`${environment.url}api/registration`, {
      email,
      password,
      name,
      photoUrl,
    });
  }
  logout() {
    this.cookieService.delete("User");
  }
  activateAccount(code: string) {
    return this.http.put(
      `${environment.url}api/registration/activate/${code}`,
      {}
    );
  }
  get currentUserValue() {
    return JSON.parse(this.cookieService.get("User"));
  }

  login(email: string, password: string) {
    var $this = this;
    return this.http
      .post(`${environment.url}api/auth/login`, {
        email,
        password,
      })
      .pipe(
        map((data) => {
          console.log();
          $this.cookieService.set("User", JSON.stringify(data));
          return data;
        })
      );
  }
  get companyID() {
    console.log("uslo");
    return this.cookieService.get("ccid");
  }
  getUsers() {
    console.log("ksdkds");
    var companyId = this.companyID;
    console.log(companyId);
    return this.http.get(`${environment.url}api/Auth/${companyId}`);
  }
}
