import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class CompanytasksService {

ccid: string;

url: string = 'https://localhost:44379/';

constructor(private http: HttpClient, private cookieService: CookieService) {
  this.ccid = this.cookieService.get('ccid');
}



public getCompanyPublicTask(){
  return this.http.get(environment.url + 'api/Calendar/' + this.ccid + '/public');
}

public getCompanyUsers(){
  return this.http.get(environment.url + 'api/Auth/' + this.ccid);
}

}
