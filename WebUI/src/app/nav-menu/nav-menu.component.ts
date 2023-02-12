import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {

  private _isExpanded = false;
  private _router: Router;

  public constructor(router: Router) {
    this._router = router;
  }

  public get isExpanded() { return this._isExpanded; }

  public ngOnInit(): void {
    this._router.events
      .pipe(filter(evt => evt instanceof NavigationEnd))
      .subscribe(_evt => this.collapse());
  }

  public collapse() {
    this._isExpanded = false;
  }

  public toggle() {
    this._isExpanded = !this._isExpanded;
  }
}
