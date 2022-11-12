import { SearchPipe } from "./../pipes/search.pipe";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
//components
import * as Components from "./index";

import { ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { CalendarModule, DateAdapter } from "angular-calendar";
import { adapterFactory } from "angular-calendar/date-adapters/date-fns";

import { FlatpickrModule } from "angularx-flatpickr";
import { RouterModule } from "@angular/router";
import { AppRoutingModule } from "app/app.routing";
import { SidebarComponent } from "../../shared/sidebar/sidebar.component";

@NgModule({
  declarations: [
    Components.AdminPannelComponent,
    Components.HomeComponent,
    Components.RegistrationComponent,
    Components.LinkActivatorComponent,
    Components.LoginComponent,
    Components.TimeSchedulerComponent,
    Components.LandingPageComponent,
    Components.DocumentationComponent,
    Components.IssueBaseComponent,
    Components.ChatComponent,
    Components.CompanyMainComponent,
    Components.NgbdModalComponent,
    SidebarComponent,
    SearchPipe,
  ],

  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    FlatpickrModule.forRoot(),
  ],
})
export class ComponentsModule {}
