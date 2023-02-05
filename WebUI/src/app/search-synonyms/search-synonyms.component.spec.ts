import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchSynonymsComponent } from './search-synonyms.component';

describe('SearchSynonymsComponent', () => {
  let component: SearchSynonymsComponent;
  let fixture: ComponentFixture<SearchSynonymsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchSynonymsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SearchSynonymsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
