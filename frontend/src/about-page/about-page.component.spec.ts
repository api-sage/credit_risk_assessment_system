import { ComponentFixture, TestBed } from '@angular/core/testing';
import { aboutPageComponent } from './about-page.component';

describe('AboutComponent', () => {
  let component: aboutPageComponent;
  let fixture: ComponentFixture<aboutPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [aboutPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(aboutPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
