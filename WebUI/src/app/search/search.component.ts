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
    this.search = new EventEmitter<string>;
  }

  public onSearch(event: Event, searchValue: string): void {
    event.stopPropagation();
    this.search.emit(searchValue);
  }

}
