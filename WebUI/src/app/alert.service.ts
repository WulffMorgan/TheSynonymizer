import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  private _subject = new Subject<Message>();

  get subject(): Subject<Message> { return this._subject; }

  public changeMessage(newMessage: string, messageType: MessageType) {
    this._subject.next({ message: newMessage, type: messageType });
  }
}

export type Message = {
  message: string;
  type: MessageType;
}

export enum MessageType {
  Success = 'success',
  Warning = 'warning',
  Error = 'danger'
}
