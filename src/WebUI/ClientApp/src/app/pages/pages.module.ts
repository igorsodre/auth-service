import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    NavMenuComponent,
    CounterComponent,
    FetchDataComponent,
    HomeComponent,
  ],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, AppRoutingModule],
  exports: [NavMenuComponent],
})
export class PagesModule {}
