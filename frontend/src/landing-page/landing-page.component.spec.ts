import { ComponentFixture, TestBed } from '@angular/core/testing';

import { landingPageComponent } from './landing-page.component';

describe('HomeComponent', () => {
  let component: landingPageComponent;
  let fixture: ComponentFixture<landingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [landingPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(landingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
