import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { VideoChatService } from "../../services/video-chat.service";
import { AuthService } from "../../services/auth.service";
import { Subject } from "rxjs";
@Component({
  selector: "app-text-chat",
  templateUrl: "./chat.component.html",
  styleUrls: ["./chat.component.scss"],
})
export class ChatComponent implements OnInit {
  @ViewChild("myvideo", { static: true }) myVideo: ElementRef;
  @ViewChild("searchInput") searchInput: ElementRef;
  myPeerId: string;
  users: any;
  user: any;
  chat: { peerID: string; messages: String }[] = [];
  friendId: string;
  activeV: boolean = false;
  constructor(
    private videoChatService: VideoChatService,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
    this.videoChatService.PrepareConnections(this.myVideo.nativeElement);
    this.videoChatService.peerId.subscribe((peerId: string) => {
      this.myPeerId = peerId;
    });

    this.reciveMessage();
    this.user = this.auth.currentUserValue.user;
    console.log(this.user);
    this.auth.getUsers().subscribe((data) => {
      this.users = data;

      console.log(this.users);
    });
  }

  reciveMessage() {
    var $this = this;
    this.videoChatService.peer.on("connection", function (conn) {
      conn.on("open", function () {
        conn.on("data", function (data) {
          $this.friendId = conn.peer;
          $this.chat.unshift({ peerID: conn.peer, messages: data });

          console.log("Received2", data);
        });
      });
    });
  }
  chatWithUser(event, id) {
    console.log(event.path[2].classList);
    event.path[2].classList.add('activeV')
    var $this = this;
    console.log("chatajmo sa: " + id);
    this.friendId = id;
    //this.activeV = !this.activeV;
  }
  checkUser(friendId: string): number {
    console.log("kasksds");
    for (var i = 0; i < this.chat.length; i++) {
      if (this.chat[i].peerID === friendId) return i;
    }
    return -1;
  }
  message: any = "";
  sendMessage() {
    console.log(this.message);
    var conn = this.videoChatService.peer.connect(this.friendId);
    var $this = this;
    conn.on("open", function () {
      conn.on("data", function (data) {
        console.log("Received", data);
      });
      $this.chat.unshift({
        peerID: $this.myPeerId,
        messages: $this.message,
      });
      conn.send($this.message);
      this.message = "";
    });
  }
}
