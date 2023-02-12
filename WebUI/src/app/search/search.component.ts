import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {

  @Output('search')
  private search;

  @ViewChild('searchInput')
  private _searchInput!: ElementRef;

  public constructor() {
    this.search = new EventEmitter<string>;
  }

  public onSearch(event: Event, searchValue: string): void {
    event.stopPropagation();
    this.search.emit(searchValue);

    (<HTMLInputElement>this._searchInput.nativeElement).select();
  }

}
