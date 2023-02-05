import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-search-synonyms',
  templateUrl: './search-synonyms.component.html',
  styleUrls: ['./search-synonyms.component.scss']
})
export class SearchSynonymsComponent {

  private http: HttpClient;
  private _wordWithSynonyms?: WordWithSynonyms;

  public constructor(http: HttpClient) {
    this.http = http;
  }

  public onSearch(searchValue: string): void {
    if (!searchValue)
      return;

    this.http.get<WordWithSynonyms>(`/api/synonyms/${searchValue}`).subscribe(result => {
      this._wordWithSynonyms = result;
    }, error => console.error(error));
  }

  get wordWithSynonyms(): WordWithSynonyms | undefined { return this._wordWithSynonyms }

}

interface WordWithSynonyms {
  word: string;
  synonyms: string[];
}
