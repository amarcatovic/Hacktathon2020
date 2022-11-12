import {
  Component,
  ChangeDetectionStrategy,
  OnInit,
  ViewChild,
  TemplateRef,
} from "@angular/core";
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours,
} from "date-fns";
import { Subject } from "rxjs";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import {
  CalendarDayViewBeforeRenderEvent,
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarMonthViewBeforeRenderEvent,
  CalendarView,
  CalendarWeekViewBeforeRenderEvent,
} from "angular-calendar";
import { TimeService } from "app/modules/services/time.service";
import { AuthService } from "app/modules/services/auth.service";

const colors: any = {
  red: {
    primary: "#ad2121",
    secondary: "#FAE3E3",
  },
  blue: {
    primary: "#1e90ff",
    secondary: "#D1E8FF",
  },
  yellow: {
    primary: "#e3bc08",
    secondary: "#FDF1BA",
  },
};

@Component({
  selector: "app-time-scheduler",
  templateUrl: "./time-scheduler.component.html",
  styleUrls: ["./time-scheduler.component.scss"],
  styles: [
    `
      .cal-month-view .bg-pink,
      .cal-week-view .cal-day-columns .bg-pink,
      .cal-day-view .bg-pink {
        background-color: hotpink !important;
      }
    `,
  ],
})
export class TimeSchedulerComponent implements OnInit {
  @ViewChild("modalContent", { static: true }) modalContent: TemplateRef<any>;

  view: CalendarView = CalendarView.Month;
  type: boolean;
  CalendarView = CalendarView;

  serverTimes: any;
  viewDate: Date = new Date();

  modalData: {
    action: string;
    event: CalendarEvent;
  };
  activeDayIsOpen: boolean = true;
  actions: CalendarEventAction[] = [
    {
      label: '<i class="fas fa-fw fa-pencil-alt"></i>',
      a11yLabel: "Edit",
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent("Edited", event);
      },
    },
    {
      label: '<i class="fas fa-fw fa-trash-alt"></i>',
      a11yLabel: "Delete",
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.events = this.events.filter((iEvent) => iEvent !== event);
        this.handleEvent("Deleted", event);
      },
    },
  ];
  events: CalendarEvent[] = [
    // {
    //   id: 0,
    //   start: subDays(startOfDay(new Date()), 1),
    //   end: addDays(new Date(), 1),
    //   title: "A 3 day event",
    //   actions: this.actions,
    //   allDay: true,
    //   color: colors.blue,
    //   cssClass: "",
    //   resizable: {
    //     beforeStart: true,
    //     afterEnd: true,
    //   },
    //   draggable: true,
    // },
  ];

  constructor(
    private modal: NgbModal,
    private timeService: TimeService,
    private userService: AuthService
  ) {}
  ngOnInit(): void {
    this.timeService.fetched.subscribe((_) => {
      this.serverData();
    });
    this.serverData();
  }
  serverData() {
    this.events = [];
    this.timeService
      .getAllTimes(this.userService.currentUserValue.user.id)
      .subscribe((data) => {
        console.log(data);
        this.serverTimes = data;
        this.assignEvents();
      });
  }

  refresh: Subject<any> = new Subject();
  // public / private events, naziv kompanije po (id),
  assignEvents() {
    this.serverTimes.forEach((element) => {
      console.log(element);
      var colorEvent = element.isPublic ? colors.blue : colors.red;
      this.setupEvents(
        element.id,
        element.title,
        colorEvent,
        new Date(element.eventStartDate),
        new Date(element.eventEndDate)
      );
    });
  }
  setupEvents(
    id: string | number,
    title1: string,
    color: any,
    startDate: Date,
    endDate: Date
  ): void {
    this.events = [
      ...this.events,
      {
        id: id,
        title: title1,
        start: startOfDay(startDate),
        end: endOfDay(endDate),
        color: color,
        actions: this.actions,
        resizable: {
          beforeStart: true,
          afterEnd: true,
        },
        draggable: true,
      },
    ];
  }
  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd,
  }: CalendarEventTimesChangedEvent): void {
    this.events = this.events.map((iEvent) => {
      if (iEvent === event) {
        return {
          ...event,
          start: newStart,
          end: newEnd,
        };
      }
      return iEvent;
    });
    console.log("uslo");
    this.timeService.updateCalendar(
      event.id,
      event.title,
      event.start,
      event.end
    );
    this.handleEvent("Dropped or resized", event);
  }

  handleEvent(action: string, event: CalendarEvent): void {
    this.modalData = { event, action };
    console.log(event);
    // this.modal.open(this.modalContent, { size: "lg" });
  }

  addEvent(): void {
    this.events = [
      ...this.events,
      {
        title: "New event",
        start: startOfDay(new Date()),
        end: endOfDay(new Date()),
        color: colors.red,
      },
    ];
  }

  deleteEvent(eventToDelete: CalendarEvent) {
    console.log(eventToDelete);
    this.events = this.events.filter((event) => event !== eventToDelete);
    this.timeService.deleteTime(eventToDelete.id);
  }
  saveEvent(eventToDelete: CalendarEvent) {
    console.log(eventToDelete);
  }
  setView(view: CalendarView) {
    this.view = view;
  }
  updateTitle(even: any) {
    console.log(even);
    this.timeService.updateCalendar(even.id, even.title, even.start, even.end);
    this.refresh.next();
  }
  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
  createEvent() {
    console.log("sjd");
  }
  dateUpdate(event) {
    console.log(event);
    this.timeService.updateCalendar(
      event.id,
      event.title,
      event.start,
      event.end
    );
    this.refresh.next();
  }
}
