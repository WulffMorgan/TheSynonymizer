import { Component } from '@angular/core';

@Component({
  selector: 'app-add-synonyms',
  templateUrl: './add-synonyms.component.html',
  styleUrls: ['./add-synonyms.component.scss']
})
export class AddSynonymsComponent {

  private _synonyms: string[] = [];

  get synonyms(): string[] { return this._synonyms; }

  public onSaveSynonymsClick() {
    console.log(this._synonyms);
  }

  public onAddInput(addSynonymInput: HTMLInputElement): void {
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
