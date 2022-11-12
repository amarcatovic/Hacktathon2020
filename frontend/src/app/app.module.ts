import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { RouterModule } from "@angular/router";
import { AppRoutingModule } from "./app.routing";

import { AppComponent } from "./app.component";
import { NavbarComponent } from "./shared/navbar/navbar.component";
import { FooterComponent } from "./shared/footer/footer.component";

import { ComponentsModule } from "./modules/components/components.module";
import { CookieService } from "ngx-cookie-service";
import { IssueBaseService } from "./modules/services/issue-base.service";

import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

//interceptors
import { JwtinterceptorInterceptor } from "./modules/components/interceptors/jwtinterceptor.interceptor";

@NgModule({
  declarations: [AppComponent, NavbarComponent, FooterComponent],
  imports: [
    HttpClientModule,
    ComponentsModule,
    BrowserModule,
    NgbModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,
  ],
  providers: [
    CookieService,
    IssueBaseService
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
