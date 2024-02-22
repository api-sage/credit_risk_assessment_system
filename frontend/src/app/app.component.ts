import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { landingPageComponent } from '../landing-page/landing-page.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, landingPageComponent,],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Home';
}
