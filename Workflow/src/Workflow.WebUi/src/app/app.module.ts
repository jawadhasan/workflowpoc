import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from './shared/shared.module';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { OnboardingModule } from './onboarding/onboarding.module';
import { InviteComponent } from './invite/invite.component';

@NgModule({
  declarations: [
    AppComponent,
    InviteComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule, 
    HttpClientModule,
    SharedModule,
    
    OnboardingModule,
    RouterModule.forRoot([
      { path: '', component: InviteComponent },
      { path: '**', component: AppComponent }    
    ])

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
