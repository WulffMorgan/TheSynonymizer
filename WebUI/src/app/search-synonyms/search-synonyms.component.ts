import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AlertService, MessageType } from '../alert.service';

@Component({
  selector: 'app-search-synonyms',
  templateUrl: './search-synonyms.component.html',
  styleUrls: ['./search-synonyms.component.scss']
})
export class SearchSynonymsComponent {

  private http: HttpClient;
  private _alertService: AlertService;
  private _wordWithSynonyms?: WordWithSynonyms;

  public constructor(http: HttpClient, alertService: AlertService) {
    this.http = http;
    this._alertService = alertService;
  }

  public onSearch(searchValue: string): void {
    if (!searchValue)
      return;

    this.http.get<WordWithSynonyms>(`/api/synonyms/${searchValue}`).subscribe(result => {
      this._wordWithSynonyms = result;
    }, error => this._alertService.changeMessage(error.message ?? 'An error occurred', MessageType.Error));
  }

  get wordWithSynonyms(): WordWithSynonyms | undefined { return this._wordWithSynonyms }

}

interface WordWithSynonyms {
  word: string;
  synonyms: string[];
}
