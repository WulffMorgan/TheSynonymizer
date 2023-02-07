import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { MessageType, NgbdAlertSelfclosing } from '../alert-selfclosing/alert-selfclosing.component';

@Component({
  selector: 'app-add-synonyms',
  templateUrl: './add-synonyms.component.html',
  styleUrls: ['./add-synonyms.component.scss']
})
export class AddSynonymsComponent {

  private _synonyms: string[] = [];
  private http: HttpClient;

  public constructor(http: HttpClient) {
    this.http = http;
  }

  @ViewChild('ngbdAlert', { static: false }) ngbdAlert!: NgbdAlertSelfclosing;

  get synonyms(): string[] { return this._synonyms; }

  public onSaveSynonymsClick() {
    if (this.synonyms.length <= 1) {
      this.ngbdAlert.changeMessage("No point in sending less than two words...", MessageType.Warning);
      return;
    }

    this.http.post('/api/synonyms', this._synonyms).subscribe(() => {
      this._synonyms.splice(0);
      this.ngbdAlert.changeMessage("Success!", MessageType.Success);
    }, error => this.ngbdAlert.changeMessage(error.message ?? 'An error occurred', MessageType.Error));
  }

  public onAddInput(addSynonymInput: HTMLInputElement): void {
    if (addSynonymInput.value)
      this._synonyms.push(addSynonymInput.value);
    addSynonymInput.value = '';
  }

  public onInputChanged(index: number, newValue: string): void {
    this.synonyms[index] = newValue;
  }

  public onInputLeave(index: number, newValue: string): void {
    if (!newValue) {
      this._synonyms.splice(index, 1);
      return;
    }
  }

  public trackSynonym(index: number, _synonym: string) {
    // This prevents the inputs to be redrawn in vain,
    // which results in loss of focus for the current input
    return index;
  }
}
