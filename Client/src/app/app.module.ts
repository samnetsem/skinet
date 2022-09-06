import { NgModule } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';

import { HomeModule } from './home/home.module';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { NgxSpinner, NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './core/interceptors/loading.interceptors';


@NgModule({
  declarations: [
    AppComponent,

  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    HomeModule,
    RouterModule,
    AppRoutingModule,
    NgxSpinnerModule

  ],
  providers: [
    { provide:HTTP_INTERCEPTORS,useClass:ErrorInterceptor,multi:true },
    { provide:HTTP_INTERCEPTORS,useClass:LoadingInterceptor,multi:true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
