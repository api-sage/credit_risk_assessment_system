import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForgotPasswdPageComponent } from './forgot-passwd-page.component';

describe('ForgotPasswdPageComponent', () => {
  let component: ForgotPasswdPageComponent;
  let fixture: ComponentFixture<ForgotPasswdPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForgotPasswdPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ForgotPasswdPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
