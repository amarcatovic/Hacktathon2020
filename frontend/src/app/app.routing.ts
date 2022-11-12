import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BrowserModule } from "@angular/platform-browser";
import { Routes, RouterModule } from "@angular/router";

import * as Components from "./modules/components";

const routes: Routes = [
  { path: "", component: Components.HomeComponent },
  {
    path: "signup",
    children: [
      { path: "register", component: Components.RegistrationComponent },
      { path: ":slug", component: Components.LinkActivatorComponent },
    ],
  },
  { path: "login", component: Components.LoginComponent },
  { path: "time", component: Components.TimeSchedulerComponent },
  { path: "menu", component: Components.LandingPageComponent },
  { path: "main", component: Components.CompanyMainComponent },
  { path: "documentation", component: Components.DocumentationComponent },
  { path: "admin", component: Components.AdminPannelComponent },
  { path: "issue-base", component: Components.IssueBaseComponent },
  { path: "chat", component: Components.ChatComponent },
];

@NgModule({
  imports: [CommonModule, BrowserModule, RouterModule.forRoot(routes)],
  exports: [],
})
export class AppRoutingModule {}
