import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { NavBarComponent } from '../nav-bar/nav-bar.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, NavBarComponent, ReactiveFormsModule],
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  onSubmit() {
    const enteredUsername = this.loginForm.get('username')?.value?.toLowerCase();
    const enteredPassword = this.loginForm.get('password')?.value;
    if (enteredUsername === 'default@email.com' && enteredPassword === 'defaultPassword') {
      this.router.navigate(['/dashboard']);
    } else {
      alert('Invalid email or password. Please enter correct details.');
      this.loginForm.reset();
    }
  }
}
