import { CookieService } from "ngx-cookie-service";
import { Component, Input } from "@angular/core";
import { NgbModal, NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { TimeService } from "../../services/time.service";
import { AuthService } from "../../services/auth.service";
@Component({
  selector: "app-modal-content",
  template: `
    <div class="modal-header">
      <h3 class="modal-title text-center font-weight-bold">Create new event</h3>
      <button
        type="button"
        class="close"
        aria-label="Close"
        (click)="activeModal.dismiss('Cross click')"
      >
        <span aria-hidden="true">&times;</span>
      </button>
    </div>
    <div class="modal-body">
      <div class="mt-2">
        <label class="titleModal font-weight-bold">Event Title</label>
        <input
          type="text"
          value=""
          name="title"
          class="form-control"
          placeholder="New Event"
          (change)="somethingChanged($event)"
        />
      </div>
      <div class="mt-2">
        <label class="titleModal font-weight-bold">Starting Date</label>

        <input
          class="form-control"
          type="datetime-local"
          name="pocetni"
          (change)="somethingChanged($event)"
        />
      </div>
      <div class="mt-2">
        <label class="titleModal font-weight-bold">Ending Date</label>
        <input
          class="form-control"
          type="datetime-local"
          name="krajnji"
          (change)="somethingChanged($event)"
        />
      </div>

      <div class="form-check mt-3">
        <label class="form-check-label">
          <input
            class="form-check-input"
            type="checkbox"
            [(ngModel)]="isChecked"
            (change)="checkValue($event)"
          />
          Private event
          <span class="form-check-sign"></span>
        </label>
      </div>
    </div>
    <div class="modal-footer p-3">
      <button
        type="button"
        class="btn btn-outline-danger"
        (click)="submitData()"
      >
        CREATE NEW EVENT
      </button>
      <button
        type="button"
        class="btn btn-outline-warning"
        (click)="activeModal.close('Close click')"
      >
        CANCEL
      </button>
    </div>
  `,
})
export class NgbdModalContent {
  title: string = "New Event";
  start: Date = new Date();
  end: Date = new Date();
  isPublic: boolean = false;
  constructor(
    public activeModal: NgbActiveModal,
    private timeService: TimeService,
    private auth: AuthService,
    private cookieService: CookieService
  ) {}
  somethingChanged(e) {
    console.log(e.target);
    switch (e.target.name) {
      case "title":
        this.title = e.target.value;
      case "krajnji":
        this.end = e.target.value;
      case "pocetni":
        this.start = e.target.value;
    }
  }
  checkValue(event: any) {
    this.isPublic = event.currentTarget.checked;
    console.log(event.currentTarget.checked);
  }
  submitData() {
    var userId = this.auth.currentUserValue.user.id;
    var firmaId = this.cookieService.get("ccid");
    this.timeService.addCalendar(
      this.title,
      this.start,
      this.end,
      this.isPublic,
      userId,
      firmaId
    );
    this.activeModal.close("Close click");
  }
}

@Component({
  selector: "app-modal-component",
  templateUrl: "./modal.component.html",
})
export class NgbdModalComponent {
  constructor(private modalService: NgbModal) {}

  open() {
    const modalRef = this.modalService.open(NgbdModalContent);
    modalRef.componentInstance.name = "World";
  }
}
