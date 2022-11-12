import { Injectable, Output } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, Observable, Subject } from "rxjs";
import { AuthService } from "./auth.service";
import { environment } from "environments/environment.prod";
@Injectable({
  providedIn: "root",
})
export class TimeService {
  @Output() fetched: Subject<void> = new Subject<void>();
  constructor(private http: HttpClient, private auth: AuthService) {}
  getAllTimes(id: string): any {
    return this.http.get(
      `${environment.url}api/Calendar/${this.auth.currentUserValue.user.id}`
    );
  }
  deleteTime(id: string | number) {}
  updateCalendar(
    id: string | number,
    title: string,
    pocetni: Date,
    krajnji: Date
  ) {
    this.http
      .put(`${environment.url}api/Calendar/${id}`, {
        pocetni,
        krajnji,
        title,
      })
      .subscribe((_) => {
        this.fetched.next();
      });
  }

  addCalendar(
    title: string,
    eventStartDate: Date,
    eventEndDate: Date,
    isPublic: boolean,
    userId: string,
    companyId: string
  ) {
    console.log(title);
    console.log(eventStartDate);
    console.log(eventEndDate);
    console.log(isPublic);
    console.log(userId);
    console.log(companyId);
    this.http
      .post(`${environment.url}api/calendar`, {
        title,
        eventStartDate,
        eventEndDate,
        isPublic,
        userId,
        companyId,
      })
      .subscribe((_) => {
        this.fetched.next();
      });
  }
}
