import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './dashboard/home/home.component';
import { TripAddComponent } from './dashboard/trip-add/trip-add.component';
import { LoginComponent } from './login/login.component';
import { TripInfoComponent } from './dashboard/trip-info/trip-info.component';
import { ActivityAddComponent } from './dashboard/activity-add/activity-add.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  { path: 'dashboard', component: HomeComponent },
  { path: 'dashboard/trips/add', component: TripAddComponent },
  { path: 'login', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard/trips/:id', component: TripInfoComponent },
  { path: 'dashboard/activity/add/:tripId', component: ActivityAddComponent },
  { path: 'users/:userId', component: ProfileComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
