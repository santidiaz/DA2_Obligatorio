import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { LoginComponent } from './sportsManager/components/permissions/login.component';
import { EventListComponent } from './sportsManager/components/event/eventList/event-list.component';
import { EventFormComponent } from './sportsManager/components/event/eventCard/event-form.component';
import { PageNotFoundComponent } from './sportsManager/components/general/pageNotFound.component';

import { SessionService } from './sportsManager/services/session.service'
import { PermissionService } from './sportsManager/services/permission.service'
import { BaseService } from './sportsManager/services/base.service'
import { AuthGuard } from './sportsManager/shared/auth.guard';
import { NavigationBar } from './sportsManager/components/navigation/nav-bar';

const appRoutes: Routes = [
  {
    path: '', redirectTo: 'login', pathMatch: 'full',
    //canActivateChild: [AuthGuard],
    /*children: [
      { path: 'event', component: EventListComponent, canActivate: [AuthGuard] },
      /*{
        path: 'city',
        loadChildren: './city/city.module#CityModule',
      },
      { path: '', redirectTo: 'event', pathMatch: 'full' }
    ]*/
  }, 
  { path: 'login', component: LoginComponent },
  { 
    path: 'event', 
    component: EventListComponent, 
    canActivate: [AuthGuard], 
    data: {
      onlyAdmin: false 
    }  
  }, 
  /*{ 
    path: 'event/:id', 
    component: EventCard, 
    canActivate: [AuthGuard], 
    data: {
      onlyAdmin: true 
    }  
  },  */
  { path: '**', component: PageNotFoundComponent }
];


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    EventListComponent,
    EventFormComponent,
    NavigationBar,
    PageNotFoundComponent
  ],
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: false } // <-- debugging purposes only
    ),
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    BaseService,
    SessionService,
    PermissionService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }





/*


const appRoutes: Routes = [
  {
    path: '', component: LayoutComponent,
    canActivate: [AuthenticationGuard],
    canActivateChild: [AuthenticationGuard],
    children: [
      { path: 'home', component: HomeComponent },
      {
        path: 'city',
        loadChildren: './city/city.module#CityModule',
      },
      { path: '', redirectTo: 'home', pathMatch: 'full' }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    TestComponent,
    LoginComponent,
    HomeComponent,
    PageNotFoundComponent,
    LayoutComponent,
    NavBarComponent
  ],
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: false } // <-- debugging purposes only
    ),
    BrowserModule,
    FormsModule,
    HttpClientModule,
    // CityModule
  ],
  providers: [
    CityService,
    SessionService,
    UserService,
    BaseApiService,
    AuthenticationGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }



*/