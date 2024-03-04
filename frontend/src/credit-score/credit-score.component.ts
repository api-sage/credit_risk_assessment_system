// import { Component, OnInit } from '@angular/core';
// import { ApiService } from '../api.service';

// interface CreditData {
//   creditScore: number;
//   bvn: string;
//   // other data fields
// }

// @Component({
//   selector: 'app-credit-score',
//   standalone: true,
//   imports: [],
//   templateUrl: './credit-score.component.html',
//   styleUrl: './credit-score.component.css'
// })
// export class CreditScoreComponent implements OnInit {
//    creditScore: number = 0;
//   rating: string = 'no rating available';
//   bvn: string = '';
//   error: string | null = null;
//   scoreHistory: any[] = []; // Array to store historical data

//   constructor(private apiService: ApiService) {} // Inject ApiService

//   ngOnInit(): void {}

//   checkScore() {
//     // Delegate logic to ApiService
//     this.apiService.getCreditScore(this.bvn)
//       .subscribe(data => {
//         this.creditScore = data.creditScore;
//         this.rating = this.getRating(this.creditScore);
//         this.updateMeter(this.creditScore);
//         this.error = null; // Clear any previous errors
//       }, error => {
//         this.error = error.message || 'An error occurred.'; // Handle API errors
//       });
//   }

//   getRating(score: number): string {
//     if (score >= 720) {
//       return 'A';
//     } else if (score >= 690) {
//       return 'B';
//     } else if (score >= 630) {
//       return 'C';
//     } else {
//       return 'D';
//     }
//   }

//   updateMeter(score: number) {
//     const percentage = Math.min(100, Math.round((score - 300) / (900 - 300) * 100));
//     const rotation = 180 * (percentage / 100);
//     const meterElement = document.getElementById('meter-element');
//     if (meterElement) {
//       meterElement.style.transform = `rotate(${rotation}deg)`;
//     }
//   }

//   viewHistory() {
//     // Implement logic to fetch and display historical data in a modal using a library like ng-bootstrap
//   }

// }
