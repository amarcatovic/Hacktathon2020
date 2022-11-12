import {
  Component,
  OnInit,
  Renderer2,
  ElementRef,
  ViewChild,
} from "@angular/core";
import { Subscription } from "rxjs/Subscription";
import { Location } from "@angular/common";
import { NavbarComponent } from "./shared/navbar/navbar.component";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  private _router: Subscription;
  @ViewChild(NavbarComponent) navbar: NavbarComponent;

  constructor(
    private renderer: Renderer2,
    private element: ElementRef,
    public location: Location
  ) {}
  ngOnInit() {
    var navbar: HTMLElement = this.element.nativeElement.children[0]
      .children[0];

    this.renderer.listen("window", "scroll", (event) => {
      const number = window.scrollY;
      if (number > 150 || window.pageYOffset > 150) {
        // add logic
      } else {
        // remove logic
      }
    });
  }
}
