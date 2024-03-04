import { Component, ElementRef, ViewChild } from '@angular/core';
import { RouterLink, RouterLinkActive, Router } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink, RouterLinkActive,],
  templateUrl: './dashboard.component.html',



})
export class DashboardComponent  {
  @ViewChild('textInput') textInput!: ElementRef<HTMLInputElement>;
  @ViewChild('userScore') userScore!: ElementRef<HTMLDivElement>;
  @ViewChild('needle') needle!: ElementRef<SVGElement>;

  maxScore: number = 900;
  gaugeRotation: number = 0; // Initial rotation for the gauge
  needleRotation: number = 0; // Initial rotation for the needle

  constructor() { }

  showScore(): void {
    const inputValue: number = parseFloat(this.textInput.nativeElement.value);
    if (!isNaN(inputValue)) {
      // Calculate gauge rotation
      const scorePercent: number = (inputValue - 300) / (this.maxScore - 300);
      this.gaugeRotation = scorePercent * 180; // Assuming the meter is 180 degrees
      
      // Calculate needle rotation
      this.needleRotation = this.gaugeRotation;
      
      // Update displayed score
      this.userScore.nativeElement.innerText = inputValue.toString();
    }
  }
}



