import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { MainViewComponent } from './main-view/main-view.component';
import { CitySearchComponent } from './city-search/city-search.component';
import { CurrentLocationComponent } from './current-location/current-location.component';

import { LoginComponent } from './login';

import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { fakeBackendProvider } from './_helpers';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AgmCoreModule } from '@agm/core';

// Toast library
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { RegisterComponent } from './register/register.component';
import { PredictionListComponent } from './prediction-list/prediction-list.component';

import { ImageListComponent } from './image-list/image-list.component';
import { ImageUploadComponent } from './image-upload/image-upload.component';

// Infinite scroll
import { InfiniteScrollModule } from 'ngx-infinite-scroll';


import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { AccountComponent } from './account/account.component';
import { UserListComponent } from './user-list/user-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    MainViewComponent,
    MainViewComponent,
    CitySearchComponent,
    CurrentLocationComponent,
    LoginComponent,
    RegisterComponent,
    PredictionListComponent,
    ImageListComponent,
    ImageUploadComponent,
    AccountComponent,
    UserListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
    FormsModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCFcgapJG17nwAFC0Yohs0x6Z9IsBwclq4',
      libraries: ['places']
    }),
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 2000,
      positionClass: 'toast-bottom-right'
    }),
    InfiniteScrollModule,
    AngularFontAwesomeModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    // provider used to create fake backend
    fakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
