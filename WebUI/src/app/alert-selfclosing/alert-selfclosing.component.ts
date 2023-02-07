import { Component, OnInit, ViewChild } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { NgbAlert, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIf } from '@angular/common';

@Component({
  selector: 'ngbd-alert-selfclosing',
  standalone: true,
  imports: [NgIf, NgbAlertModule],
  templateUrl: './alert-selfclosing.component.html',
})
export class NgbdAlertSelfclosing implements OnInit {
  private _success = new Subject<string>();

  staticAlertClosed = false;
  successMessage = '';
  messageType = '';

  @ViewChild('selfClosingAlert', { static: false }) selfClosingAlert!: NgbAlert;

  ngOnInit(): void {
    this._success.subscribe((message) => (this.successMessage = message));
    this._success.pipe(debounceTime(5000)).subscribe(() => {
      if (this.selfClosingAlert) {
        this.selfClosingAlert.close();
      }
    });
  }

  public changeMessage(newMessage: string, messageType: MessageType) {
    this.messageType = messageType;
    this._success.next(newMessage);
  }
}

export enum MessageType {
  Success = 'success',
  Warning = 'warning',
  Error = 'danger'
}
