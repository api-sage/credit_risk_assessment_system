import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, ],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})
export class LoginComponent {

}
