import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NavBarComponent } from '../nav-bar/nav-bar.component';


@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [RouterLink, RouterOutlet,RouterLinkActive,NavBarComponent],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.css'
})
export class landingPageComponent {
  imagePath: string = 'assets/image.jpg';
}
