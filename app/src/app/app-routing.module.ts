import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './dashboard/home/home.component';
import { TripAddComponent } from './dashboard/trip-add/trip-add.component';
import { TripComponent } from './dashboard/trip/trip.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: 'dashboard', component: HomeComponent },
  { path: 'dashboard/trips/add', component: TripAddComponent },
  { path: 'dashboard/trips/:id', component: TripComponent },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
