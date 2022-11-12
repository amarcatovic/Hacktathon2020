import { FormBuilder, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";
import { CompanyService } from "./../../services/company.service";
import { Component, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";

@Component({
  selector: "app-landing-page",
  templateUrl: "./landing-page.component.html",
  styleUrls: ["./landing-page.component.scss"],
})
export class LandingPageComponent implements OnInit {
  //JSON.parse(this.cookieService.get("User"));
  constructor(
    private companyService: CompanyService,
    private cookieService: CookieService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {}

  userCompanies: any[];

  display: number;

  createCompany: FormGroup;
  joinCompany: FormGroup;

  ngOnInit(): void {
    this.display = 0;

    this.companyService.goBackToLanding.subscribe((num: number) => {
      this.display = num;
      this.fetchCompanies();
    });

    this.fetchCompanies();

    this.createCompany = this.formBuilder.group({
      email: ["", [Validators.required]],
      password: ["", [Validators.required]],
    });

    this.joinCompany = this.formBuilder.group({
      email: ["", [Validators.required]],
    });
  }

  fetchCompanies() {
    this.companyService.getUserCompanies().subscribe((data: any[]) => {
      this.userCompanies = data;
    });
  }

  displayOpt(opt: number) {
    this.display = opt;
  }

  selectCompany(companyId: string) {
    this.cookieService.set("ccid", companyId);
    this.router.navigate(["main"]);
  }

  SubmitJoinCompany(e: any) {
    this.companyService.userJoinRequest(this.joinCompany.get("email").value);
  }

  SubmitCreate(e: any) {
    let company = {
      name: this.createCompany.get("email").value,
      city: this.createCompany.get("password").value,
    };

    this.companyService.userCreatecompany(company);
  }
}
