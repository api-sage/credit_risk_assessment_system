import { Component, Input } from '@angular/core';
import { CreditHistory } from '../../models/user-details';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from '../../dashboard/dashboard.component';


@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [CommonModule, DashboardComponent],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.css'
})
export class ModalComponent {
  @Input()  creditData! : CreditHistory[];
  closeModal() {
    const modal = document.getElementById("static-modal");
    if (modal) {
      modal.style.display ="none"
    }

  }

  openModal() {
    const modal = document.getElementById("static-modal");
    console.log(this.creditData);
    
    if (modal) {
      modal.style.display ="flex"
    }

 

  }


}






