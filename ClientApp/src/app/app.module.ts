import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_guards/index';
import { UserService, AuthenticationService} from './_services/index'; // AlertService, AuthenticationService,
import { JwtInterceptor } from './_helpers/index';
import { HomeDetailComponent } from './home/home-detail/home-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RegisterComponent,
    LoginComponent,
    HomeDetailComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'register/:id', component: RegisterComponent, canActivate: [AuthGuard] },//, 
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] }, //, canActivate: [AuthGuard]
      //{ path: 'counter', component: CounterComponent, canActivate: [AuthGuard]},
      //{ path: 'fetch-data', component: FetchDataComponent },//, canActivate: [AuthGuard]
      { path: 'home/home-detail/:id', component: HomeDetailComponent, canActivate: [AuthGuard] }, //, canActivate: [AuthGuard]
      
    ])
  ],
  providers: [
    AuthGuard,
    UserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    AuthenticationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
