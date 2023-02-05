import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RouterModule } from '@angular/router';
import { SearchSynonymsComponent } from './search-synonyms/search-synonyms.component';
import { SearchComponent } from './search/search.component';
import { AddSynonymsComponent } from './add-synonyms/add-synonyms.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SearchSynonymsComponent,
    SearchComponent,
    AddSynonymsComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, NgbModule,
    RouterModule.forRoot([
      { path: '', component: SearchSynonymsComponent, pathMatch: 'full' },
      { path: 'add', component: AddSynonymsComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
