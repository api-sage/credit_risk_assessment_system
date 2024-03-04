import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreditscoreComponent } from './creditscore.component';

describe('CreditscoreComponent', () => {
  let component: CreditscoreComponent;
  let fixture: ComponentFixture<CreditscoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreditscoreComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreditscoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
