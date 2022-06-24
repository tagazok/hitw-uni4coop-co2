import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './dashboard/home/home.component';
import { TripListComponent } from './dashboard/trip-list/trip-list.component';
import { TripAddComponent } from './dashboard/trip-add/trip-add.component';

import { HttpClient, HttpClientModule } from '@angular/common/http';

import { MatButtonModule } from '@angular/material/button';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    TripListComponent,
    TripAddComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatButtonModule
  ],
  providers: [
    // { provide: LocationStrategy, useClass: HashLocationStrategy }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
