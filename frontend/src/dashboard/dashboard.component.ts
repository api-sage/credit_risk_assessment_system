// import { Component, OnInit } from '@angular/core';
// import { RouterLink, RouterLinkActive, Router } from '@angular/router';
// import { ApiService } from '../api.service';
// import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
// import { CreditHistory } from '../models/user-details';
// import { ModalComponent } from '../app/modal/modal.component';
// import { NgClass } from '@angular/common';
// @Component({
//   selector: 'app-dashboard',
//   standalone: true,
//   imports: [RouterLink, RouterLinkActive, ReactiveFormsModule, ModalComponent, NgClass],
//   templateUrl: './dashboard.component.html',
// })
// export class DashboardComponent {
//   creditScore!: number;
//   creditRating!: string;
//   isRed: boolean= false;
//   isBlue: boolean= false;
//   isGreen: boolean= false;
//   isYellow: boolean= false;
  

//   bvnForm: FormGroup = new FormGroup({
//     bvn: new FormControl(''),
//   });

//   fetchedCreditHistory!: CreditHistory[];

//   constructor(private assess: ApiService) {}
 

//   chnageBg(creditRating: string){
//     if (creditRating === "F - High Risk") {
//       this.isRed=true;
//       this.isYellow=false
//       this.isGreen=false;
//       this.isBlue=false 
//       console.log('Credit Rating:', creditRating);
//   // Your existing logic...
//   console.log('isRed:', this.isRed);
//   console.log('isGreen:', this.isGreen);
//   console.log('isBlue:', this.isBlue);
//   console.log('isYellow:', this.isYellow);         

//     } else if (creditRating === "C - Fair Risk"){
//       this.isRed=false;
//       this.isYellow=false
//       this.isGreen=false;
//       this.isBlue=true
//     }else if (creditRating === "B - Moderate Risk"){
//       this.isRed=false;
//       this.isYellow=true
//       this.isGreen=false;
//       this.isBlue=false
//     }else if (creditRating === "A - Low Risk"){
//       this.isRed=false;
//       this.isYellow=false
//       this.isGreen=true;
//       this.isBlue=false
//     }
//   }
//   onSubmit(): void {

//     this.assess.assessCreditHistory(this.bvnForm.value).subscribe(
//       (response) => {
//         console.log(response);
//         this.creditScore = response.data.predictedCreditScore;
//         this.creditRating = response.data.creditRating;
//         this.chnageBg(this.creditRating)
//       },
//       (error) => {
//         console.error(error);
//       }
//     );

//     this.assess.getAssessedCreditHistory(this.bvnForm.value).subscribe(
//       (response)=>{
//         console.log(response);

//       },
//       (error)=>{
//         console.error(error);

//       }
//     )
//   }

//   // onGetAccess()

//   openModal() {
//     const modal = document.getElementById('static-modal');
//     this.assess.getAssessedCreditHistory(this.bvnForm?.value).subscribe(
//       (response) => {
//         this.fetchedCreditHistory = response?.data;
//         console.log(this.fetchedCreditHistory);
//       },
//       (error) => {
//         console.error(error);
//       }
//     );

//     if (modal) {
//       modal.style.display = 'flex';
//     }
//   }
// }

import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, Router } from '@angular/router';
import { ApiService } from '../api.service';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreditHistory } from '../models/user-details';
import { ModalComponent } from '../app/modal/modal.component';
import { CommonModule, NgClass } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
   imports: [RouterLink, RouterLinkActive, ReactiveFormsModule, ModalComponent, NgClass,CommonModule],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent {
  creditScore!: number;
  creditRating!: string;
  isRed: boolean = false;
  isBlue: boolean = false;
  isGreen: boolean = false;
  isYellow: boolean = false;

  bvnForm: FormGroup = new FormGroup({
        bvn: new FormControl(''),
     });

  fetchedCreditHistory!: CreditHistory[];

  isLoading = false;

  constructor(private fb: FormBuilder, private assess: ApiService) {
    this.bvnForm = this.fb.group({
      bvn: ['', [Validators.required, Validators.pattern(/^\d{11}$/)]],
    });
  }

  chnageBg(creditRating: string): void {
    if (creditRating === 'F - High Risk') {
      this.isRed = true;
      this.isYellow = false;
      this.isGreen = false;
      this.isBlue = false;
    } else if (creditRating === 'C - Fair Risk') {
      this.isRed = false;
      this.isYellow = false;
      this.isGreen = false;
      this.isBlue = true;
    } else if (creditRating === 'B - Moderate Risk') {
      this.isRed = false;
      this.isYellow = true;
      this.isGreen = false;
      this.isBlue = false;
    } else if (creditRating === 'A - Low Risk') {
      this.isRed = false;
      this.isYellow = false;
      this.isGreen = true;
      this.isBlue = false;
    }
  }

  onSubmit(): void {
    if (this.bvnForm.invalid) {
      alert('Please enter a valid 11-digit BVN.');
    } else {
      this.isLoading = true; // Set loading flag to true
      this.assess.assessCreditHistory(this.bvnForm.value).subscribe(
        (response) => {
          console.log(response);
          this.creditScore = response.data.predictedCreditScore;
          this.creditRating = response.data.creditRating;
          this.chnageBg(this.creditRating);
        },
        (error) => {
          console.error(error);
        },
        () => {
          this.isLoading = false; // Set loading flag to false when request is complete
        }
      );

      this.assess.getAssessedCreditHistory(this.bvnForm.value).subscribe(
        (response) => {
          console.log(response);
        },
        (error) => {
          console.error(error);
        }
      );
    }
  }

  

  openModal(): void {
    if (this.bvnForm.invalid) {
      alert('Please enter a valid 11-digit BVN before checking the risk profile.');
    } else {
      const modal = document.getElementById('static-modal');
      this.assess.getAssessedCreditHistory(this.bvnForm?.value).subscribe(
        (response) => {
          this.fetchedCreditHistory = response?.data;
          console.log(this.fetchedCreditHistory);
        },
        (error) => {
          console.error(error);
        }
      );
  
      if (modal) {
        modal.style.display = 'flex';
      }
    }
  }
  
}

