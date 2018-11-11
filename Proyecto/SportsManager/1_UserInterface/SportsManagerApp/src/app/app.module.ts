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
import { SessionService } from './sportsManager/services/session.service';
import { PermissionService } from './sportsManager/services/permission.service';
import { BaseService } from './sportsManager/services/base.service';
import { AddUserComponent } from './sportsManager/components/users/adduser/adduser.component';
import { UserService } from './sportsManager/services/user.service';
import { ListUsersComponent } from './sportsManager/components/users/listUsers/listusers.component';
import { UserEditComponent } from './sportsManager/components/users/useredit/useredit.component';
import { AddSportComponent } from './sportsManager/components/sport/addsport/addsport.component';
import { SportService } from './sportsManager/services/sport.service';
import { ListSportsComponent } from './sportsManager/components/sport/listsports/listsports.component';
import { EditSportComponent } from './sportsManager/components/sport/editsport/editsport.component';

import { SessionService } from './sportsManager/services/session.service'
import { PermissionService } from './sportsManager/services/permission.service'
import { BaseService } from './sportsManager/services/base.service'

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
  { path: 'event', component: EventListComponent },
  //{ path 'team', componet: MyTEmasComponetn },
  { path: 'addUser', component: AddUserComponent },
  { path: 'listUsers', component: ListUsersComponent },
  { path: 'editUser', component: UserEditComponent },
  { path: 'addSport', component: AddSportComponent },
  { path: 'listSport', component: ListSportsComponent },
  { path: 'editsport', component: EditSportComponent },
  { path: '',
    redirectTo: '/login',
    pathMatch: 'full'
  },  
  { path: '404', component: PageNotFoundComponent },
  { path: '**', component: PageNotFoundComponent }
];


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    EventListComponent,
    AddUserComponent,
    ListUsersComponent,
    UserEditComponent,
    AddSportComponent,
    ListSportsComponent,
    EditSportComponent,
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
      EventService,
      UserService,
      SportService
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