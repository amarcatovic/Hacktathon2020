import { environment } from './../../../environments/environment.prod';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { Injectable, Output } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

url: string = 'https://localhost:44379/';
userCookie: any;

@Output() goBackToLanding: Subject<number> = new Subject<number>();

constructor(private http: HttpClient, private cookieService: CookieService, private router: Router) {
  this.userCookie = JSON.parse(this.cookieService.get("User"));
}

public getUserCompanies(){
  return this.http.get(environment.url + 'api/Companies/user/' + this.userCookie.user.id);
}

public userCreatecompany(company: any){
  this.http.post(environment.url + 'api/Companies', {
    name: company.name,
    city: company.city,
    userId: this.userCookie.user.id
  }).subscribe(_ => {
    this.goBackToLanding.next(0);
  });

}

public userJoinRequest(companyId: string){
  this.http.put(environment.url + 'api/Companies/join', {
    userId: this.userCookie.user.id,
    code: companyId
  }).subscribe(_ => {
    this.goBackToLanding.next(0);
  });
}
}
