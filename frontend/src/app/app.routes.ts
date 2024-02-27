import { Routes } from '@angular/router';
import { landingPageComponent } from '../landing-page/landing-page.component';
import { CreateAccountComponent } from '../create-account/create-account.component';
import { LoginComponent } from '../login-page/login-page.component';
import { aboutPageComponent } from '../about-page/about-page.component';
import { DashboardComponent } from '../dashboard/dashboard.component';

 export const routes: Routes = [
    { path: 'landing-page', component: landingPageComponent },
    { path: 'create-account', component: CreateAccountComponent },
    { path: 'login', component: LoginComponent },
    { path: 'about-page', component: aboutPageComponent },
    { path: 'dashboard', component: DashboardComponent, }, // Protect dashboard with AuthGuard
    
];

