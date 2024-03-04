import { Component,  } from '@angular/core';
import { RouterLink, RouterLinkActive, Router } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink, RouterLinkActive,],
  templateUrl: './dashboard.component.html',



})
export class DashboardComponent  {


}

