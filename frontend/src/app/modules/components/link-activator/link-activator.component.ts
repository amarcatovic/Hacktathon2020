import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, ParamMap, Router } from "@angular/router";
import { AuthService } from "../../services/auth.service";
@Component({
  selector: "app-link-activator",
  templateUrl: "./link-activator.component.html",
  styleUrls: ["./link-activator.component.css"],
})
export class LinkActivatorComponent implements OnInit {
  public currentSlug: string;
  constructor(
    private route: ActivatedRoute,
    private auth: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      let slug = params.get("slug");
      this.currentSlug = slug;
    });
    this.auth.activateAccount(this.currentSlug).subscribe((data) => {
      console.log(data);
      this.router.navigate([""]);
    });
  }
}
