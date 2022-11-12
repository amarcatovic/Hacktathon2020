import { ElementRef, Injectable, Output } from "@angular/core";
import { environment } from "environments/environment.prod";
import { AuthService } from "./auth.service";
import { Subject } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class VideoChatService {
  peer: any;
  myStream: any;
  @Output() peerId: Subject<string>;

  /**
   * Method that generates random connection code
   * @returns Randomly generated code between 0 and 10000
   */
  private GenerateConnectionCode(): string {
    return String(Math.floor(Math.random() * Math.floor(10000)));
  }

  constructor(private auth: AuthService) {
    this.peerId = new Subject<string>();
    // const code = this.GenerateConnectionCode();
    console.log(this.auth.currentUserValue.user.id);
    this.peer = new Peer(this.auth.currentUserValue.user.id, {
      host: "peerjs-server.herokuapp.com",
      secure: true,
      port: 443,
    }); /*code, {
      host: 'localhost',
      port: 9000,
      path: '/myapp'
    });*/
    console.log(this.peer.id);
  }

  /**
   * Method that prepares and seeks connections
   * @param video HTML Video element reference
   * @returns users connection Id
   */
  PrepareConnections(video: any): void {
    var n = <any>navigator;
    n.getUserMedia =
      n.getUserMedia || n.webkitGetUserMedia || n.mozGetUserMedia;

    this.peer.on("call", (call: any) => {
      n.getUserMedia(
        { video: true, audio: true },
        (stream: any) => {
          var answ = confirm("da ne");
          console.log(answ);
          if (answ) {
            call.answer(stream);
          }

          call.on("stream", (remoteStream: any) => {
            // remoteStream.getAudioTracks()[0].enabled = false;
            this.myStream = remoteStream;
            video.srcObject = remoteStream;
            video.play();
          });
        },
        (err: any) => {
          alert(err);
        }
      );
    });

    console.log(this.peer);
    setTimeout((_) => {
      this.peerId.next(this.peer.id);
    }, 3000);
  }

  StartVideoCall(video: any, partnerId: string): void {
    var n = <any>navigator;

    n.getUserMedia =
      n.getUserMedia || n.webkitGetUserMedia || n.mozGetUserMedia;

    n.getUserMedia(
      { video: true, audio: true },
      (stream: any) => {
        var call = this.peer.call(partnerId, stream);
        call.on("stream", (remoteStream: any) => {
          this.myStream = remoteStream;
          video.srcObject = remoteStream;
          video.play();
        });
      },
      (err: any) => {
        alert(err);
      }
    );
  }
}
