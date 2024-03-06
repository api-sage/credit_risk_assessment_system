import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, Router } from '@angular/router';
import { ApiService } from '../api.service';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CreditHistory } from '../models/user-details';
import { ModalComponent } from '../app/modal/modal.component';
@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, ReactiveFormsModule, ModalComponent],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent {
  creditScore!: number;
  creditRating!: string;

  bvnForm: FormGroup = new FormGroup({
    bvn: new FormControl(''),
  });

  fetchedCreditHistory!: CreditHistory[];

  constructor(private assess: ApiService) {}

  onSubmit(): void {


    this.assess.assessCreditHistory(this.bvnForm.value).subscribe(
      (response) => {
        console.log(response);
        this.creditScore = response.data.predictedCreditScore;
        this.creditRating = response.data.creditRating;
      },
      (error) => {
        console.error(error);
      }
    );

    this.assess.getAssessedCreditHistory(this.bvnForm.value).subscribe(
      (response)=>{
        console.log(response);

      },
      (error)=>{
        console.error(error);

      }
    )
  }

  // onGetAccess()

  openModal() {
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
