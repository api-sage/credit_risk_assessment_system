import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BvnFieldComponent } from './bvn-field.component';

describe('BvnFieldComponent', () => {
  let component: BvnFieldComponent;
  let fixture: ComponentFixture<BvnFieldComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BvnFieldComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BvnFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
