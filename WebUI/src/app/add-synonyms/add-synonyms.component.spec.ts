import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSynonymsComponent } from './add-synonyms.component';

describe('AddSynonymsComponent', () => {
  let component: AddSynonymsComponent;
  let fixture: ComponentFixture<AddSynonymsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddSynonymsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddSynonymsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
