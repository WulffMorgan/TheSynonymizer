import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {

  @Output('search')
  private search;

  public constructor() {
    this.search = new EventEmitter;
  }

  public onSearch(searchValue: string): void {
    this.search.emit(searchValue);
  }

}
