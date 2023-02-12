import { Component, OnInit, ViewChild } from '@angular/core';
import { debounceTime } from 'rxjs/operators';
import { NgbAlert, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIf } from '@angular/common';
import { AlertService, Message } from '../alert.service';

@Component({
  selector: 'ngbd-alert-selfclosing',
  standalone: true,
  imports: [NgIf, NgbAlertModule],
  templateUrl: './alert-selfclosing.component.html',
})
export class NgbdAlertSelfclosing implements OnInit {

  private _message?: Message;
  private _alertService: AlertService;

  @ViewChild('selfClosingAlert', { static: false })
  private _selfClosingAlert!: NgbAlert;

  public constructor(alertService: AlertService) {
    this._alertService = alertService;
  }

  public get message() { return this._message; }

  public ngOnInit(): void {
    this._alertService.subject.subscribe((message) => (this._message = message));
    this._alertService.subject.pipe(debounceTime(5000)).subscribe(() => {
      if (this._selfClosingAlert) {
        this._selfClosingAlert.close();
      }
    });
  }

  public clear(): void {
    this._message = undefined;
  }

}
