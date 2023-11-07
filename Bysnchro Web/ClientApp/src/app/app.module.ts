import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AddAccountComponent } from './add-account/add-account.component';
import { GetUserInfoComponent } from './get-user-info/get-user-info.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AddAccountComponent,
    GetUserInfoComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: GetUserInfoComponent, pathMatch: 'full' },
      { path: 'AddAccount', component: AddAccountComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
