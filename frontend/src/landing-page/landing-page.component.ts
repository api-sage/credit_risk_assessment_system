import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [RouterLink, RouterOutlet,],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.css'
})
export class landingPageComponent {
  imagePath: string = 'assets/image.jpg';
}
