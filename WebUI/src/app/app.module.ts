import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RouterModule } from '@angular/router';
import { SearchSynonymsComponent } from './search-synonyms/search-synonyms.component';
import { SearchComponent } from './search/search.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SearchSynonymsComponent,
    SearchComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, NgbModule,
    RouterModule.forRoot([
      { path: '', component: SearchSynonymsComponent, pathMatch: 'full' }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
