import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { ApiService } from '../api.service';



@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLinkActive,RouterLink,],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent { 
  bvn: string = '';
  creditScore: string = '000';
  creditRating: string = 'No rating available';

  constructor(private apiService: ApiService) {}

  checkScore() {
    // Perform validation on BVN
    if (this.bvn.length !== 11 || isNaN(Number(this.bvn))) {
      alert('Invalid BVN');
      return; 

}

  // Call API to fetch credit score
  this.apiService.getCreditScore(this.bvn).subscribe((data: any) => {
    // Update credit score and rating
    this.creditScore = data.creditScore;
    this.calculateRating(data.creditScore);
  }, error => {
    console.error('Error fetching credit score:', error);
  });
}

calculateRating(creditScore: number) {
  if (creditScore >= 720) {
    this.creditRating = 'A';
  } else if (creditScore >= 690) {
    this.creditRating = 'B';
  } else if (creditScore >= 630) {
    this.creditRating = 'C';
  } else {
    this.creditRating = 'D';
  }
}

viewHistory() {
  // Implement logic to display modal with additional details
}
}
