import { Component, ElementRef, NgZone, OnInit, ViewChild } from "@angular/core";
import { animate, style, transition, trigger } from "@angular/animations";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AuthService } from "../../services/auth.service";
import { Router, RouterModule } from "@angular/router";
import { first } from "rxjs/operators";
@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
  animations: [
    trigger("myInsertRemoveTrigger", [
      transition(":enter", [
        style({ opacity: 0 }),
        animate("500ms", style({ opacity: 1 })),
      ]),
      transition(":leave", [
        animate("500ms", style({ transform: "rotate(-200px)" })),
      ]),
    ]),
  ],
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;

  constructor(
    private zone: NgZone,
    private formBuilder: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.googleInitialize();
    this.loginForm = this.formBuilder.group({
      email: ["", [Validators.email, Validators.required]],
      password: [
        "",
        Validators.pattern(/^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z]{6,}$/),
        ,
        Validators.required,
      ],
    });
  }
  get formData() {
    return this.loginForm.controls;
  }
  Submit(event) {
    event.preventDefault();
    if (this.loginForm.invalid) {
      return;
    }

    this.auth
      .login(this.formData.email.value, this.formData.password.value)
      .subscribe((data) => {
        this.router.navigate(["menu"]);
        console.log(data);
      });
  }
  cookie() {
    console.log(this.auth.currentUserValue);
  }


  auth2: any;
  error: any;
  @ViewChild("loginRef", { static: true }) loginElement: ElementRef;
  googleInitialize() {
    window["googleSDKLoaded"] = () => {
      window["gapi"].load("auth2", () => {
        this.auth2 = window["gapi"].auth2.init({
          client_id:
            "802237008617-89bgli7rjuuq2d221coungdvnh3hcjc4.apps.googleusercontent.com",
          cookie_policy: "single_host_origin",
          scope: "profile email",
        });
        this.prepareLogin();
      });
    };
    (function (d, s, id) {
      var js,
        fjs = d.getElementsByTagName(s)[0];
      if (d.getElementById(id)) {
        return;
      }
      js = d.createElement(s);
      js.id = id;
      js.src = "https://apis.google.com/js/platform.js?onload=googleSDKLoaded";
      fjs.parentNode.insertBefore(js, fjs);
    })(document, "script", "google-jssdk");
  }

  prepareLogin() {
    this.auth2.attachClickHandler(
      this.loginElement.nativeElement,
      {},
      (googleUser) => {
        let profile = googleUser.getBasicProfile();
        console.log("Token || " + googleUser.getAuthResponse().id_token);
        // this.show = true;
        // this.Name = profile.getName();
        console.log("Image URL: " + profile.getImageUrl());
        console.log("Email: " + profile.getEmail());
        console.log("Full name: " + profile.getName());
        console.log(profile);
        console.log("OT: " + profile.OT);
        console.log(googleUser.getAuthResponse());

        this.auth
        .login(profile.getEmail(), profile.OT)
        .subscribe((data) => {
          this.zone.run(() => {
            this.router.navigate(['/menu']);
        });
        });

      },
      (error) => {
        alert(JSON.stringify(error, undefined, 2));
      }
    );
  }
}
