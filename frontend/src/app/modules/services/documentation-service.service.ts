import { environment } from './../../../environments/environment.prod';
import { Subject } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { Injectable, Output } from '@angular/core';
import { RequestOptions } from '@angular/http/src/base_request_options';

@Injectable({
  providedIn: 'root'
})
export class DocumentationServiceService {

@Output() liked: Subject<boolean> = new Subject<boolean>();
 
ccid: string;

constructor(private http: HttpClient, private cookieService: CookieService) {
  this.ccid = this.cookieService.get('ccid');
}

getAllDocs(){
  return this.http.get(environment.url + 'api/Documentation/' + this.ccid);
}

likeDoc(id: number){
  this.http.put(environment.url + 'api/Documentation/star/' + id, {})
    .subscribe(_ => {
      this.liked.next(true);
    });
}

uploadDoc(file: File, name: string){
  const formData: FormData = new FormData();
  formData.append('PdfFile', file);
  formData.append('Name ', name);
  formData.append('CompanyId  ', this.ccid);


  var headers = new Headers();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');

  //let options = new RequestOptions({ headers: headers });

  this.http.post(environment.url + 'api/documentation', formData)
    .subscribe(r => {
      alert(r)
    }),
    (error) => {
      alert(error)
    };

}

}
