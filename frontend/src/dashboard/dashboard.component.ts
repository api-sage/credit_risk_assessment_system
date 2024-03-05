import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, Router } from '@angular/router';
import { ApiService } from '../api.service';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, ReactiveFormsModule],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent  {
  creditScore!: number
  creditRating!: string

  bvnForm: FormGroup= new FormGroup({
    bvn: new FormControl('')
  })

  constructor(private assess: ApiService){}

  onSubmit(): void{
    console.log(this.bvnForm.value);
    
    this.assess.assessCreditHistory(this.bvnForm.value).subscribe(
      (response)=>{
        console.log(response);
        this.creditScore= response.data.predictedCreditScore;
        this.creditRating=response.data.creditRating;
      },
      (error)=>{
        console.error(error);
        
      }
    )

    // this.assess.getAssessedCreditHistory(this.bvnForm.value).subscribe(
    //   (response)=>{
    //     console.log(response);
        
    //   },
    //   (error)=>{
    //     console.error(error);
        
    //   }
    // )
  }
}
