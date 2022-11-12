import { Router } from '@angular/router';
import { Component, ElementRef, NgZone, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { PasswordValidator } from "../../validators/password.validator";
import { animate, style, transition, trigger } from "@angular/animations";
import { AuthService } from "../../services/auth.service";
import { first } from "rxjs/operators";
@Component({
  selector: "app-registration",
  templateUrl: "./registration.component.html",
  styleUrls: ["./registration.component.scss"],
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
export class RegistrationComponent implements OnInit {
  isShown: boolean;
  isRegister: boolean = false;
  public registrationForm: FormGroup;
  public codeValidationForm: FormGroup;
  public formSubmitted: boolean;
  public error: "";
  constructor(private formBuilder: FormBuilder, private auth: AuthService, private router: Router, private zone: NgZone) {}

  ngOnInit(): void {
    this.isShown = false;
    this.formBuilderInit();
    this.googleInitialize();
  }

  formBuilderInit() {
    this.codeValidationForm = this.formBuilder.group({ code: [""] });
    this.registrationForm = this.formBuilder.group(
      {
        fullName: ["", [Validators.required, Validators.minLength(3)]],
        password: [
          "",
          [
            Validators.pattern(
              /^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z]{6,}$/
            ),
            ,
            Validators.minLength(7),
          ],
        ],
        confirmPassword: [""],
        email: ["", [Validators.required, Validators.email]],
      },
      { validator: PasswordValidator }
    );
  }
  get formData() {
    return this.registrationForm.controls;
  }
  get codeFormData() {
    return this.codeValidationForm.controls;
  }
  SubmitCode(e) {
    e.preventDefault();
    console.log(this.codeFormData.code.value);
    this.auth
      .activateAccount(this.codeFormData.code.value)
      .subscribe((data) => {
        console.log(data);
      });
  }
  Submit(e) {
    this.isShown = !this.isShown;
    this.isRegister = true;
    e.preventDefault();
    this.formSubmitted = true;

    if (this.registrationForm.invalid) {
      return;
    }

    this.auth
      .registration(
        this.formData.email.value,
        this.formData.password.value,
        this.formData.fullName.value
      )
      .pipe(first())
      .subscribe(
        (data) => {
          console.log(data);
        },
        (error) => {
          this.error = error;
        }
      );
  }



  auth2: any;
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
      .registration(
        profile.getEmail(),
        profile.OT,
        profile.getName(),
        profile.getImageUrl()
      )
      .pipe(first())
      .subscribe(
        (data) => {
          this.zone.run(() => {
            this.router.navigate(['/login']);
        });
        },
        (error) => {
          this.error = error;
        }
      );

      },
      (error) => {
        alert(JSON.stringify(error, undefined, 2));
      }
    );
  }


}
