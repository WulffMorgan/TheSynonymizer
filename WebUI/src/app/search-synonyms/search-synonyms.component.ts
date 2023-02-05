import { Component } from '@angular/core';

@Component({
  selector: 'app-search-synonyms',
  templateUrl: './search-synonyms.component.html',
  styleUrls: ['./search-synonyms.component.scss']
})
export class SearchSynonymsComponent {

  public onSearch(searchValue: string): void {
    console.log(searchValue);
  }

}
