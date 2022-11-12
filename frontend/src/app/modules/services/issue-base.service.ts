import { environment } from './../../../environments/environment';
import { Subject } from 'rxjs';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { Injectable, Output } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class IssueBaseService {
  issue: any;

  ccid: string;

  @Output() fetchAgain: Subject<void> = new Subject<void>();

  constructor(private http: HttpClient, private cookieService: CookieService) {
    this.ccid = this.cookieService.get('ccid');
   }

  getIssues(): Observable<any> {
    return this.http.get(environment.url + 'api/IssueBase/' + this.ccid);
  }

  createIssue(questionQuestion, questionTitleCreate, code){
    this.http.post(environment.url + 'api/issuebase', {
      title: questionTitleCreate,
      content: questionQuestion,
      code: code,
      companyId: this.ccid,
      userId: (JSON.parse(this.cookieService.get("User"))).user.id
    }).subscribe(_ => {
      this.fetchAgain.next();
    });
  }

  addComment(issueId, answer, code): void{
    this.http.post(environment.url + 'api/issuebase/comment', {
      content: answer,
      issueBaseId: issueId,
      code: code,
      userId: (JSON.parse(this.cookieService.get("User"))).user.id
    }).subscribe(_ => {
      this.fetchAgain.next();
    });
  }

  getSearchResult() {
    return [
      {
        id: 1,
        title: "How how how",
        content:"<div class="+"md-form"+"><textarea id="+"comment-area"+"type="+"text"+"  ></textarea></div>",
        userName: "Unjumblee Admin Team",
        userImageUrl: null,
        code: 1,
        comments: [
          {
            id: 1,
            content: "<label >First name:</label><input><br><br>",
            userName: "Unjumblee Admin Team",
            userImageUrl: null,
            code: 2
          }
        ]
      },
      {
        id: 2,
        title: "How how how",
        content:"TEXT TEXT",
        userName: "Unjumblee Admin Team",
        userImageUrl: null,
        code: 0,
        comments: [
          {
            id: 2,
            content: "codeeee",
            userName: "Unjumblee Admin Team",
            userImageUrl: null,
            code: 0
          }
        ]
      },
    ]
  }
}
