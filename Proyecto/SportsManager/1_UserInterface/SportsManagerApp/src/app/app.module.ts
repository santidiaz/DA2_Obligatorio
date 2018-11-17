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
import { AddTeamComponent } from './sportsManager/components/team/addteam/addteam.component';
import { TeamService } from './sportsManager/services/team.service';
import { ListTeamsComponent } from './sportsManager/components/team/listteams/listteams.component';
import { EditTeamComponent } from './sportsManager/components/team/editteam/editteam.component';


import { AuthGuard } from './sportsManager/shared/auth.guard';
import { NavigationBar } from './sportsManager/components/navigation/nav-bar';
import { EventService } from './sportsManager/services/event.service';
import { AddEventComponent } from './sportsManager/components/event/addevent/addevent.component';
import { AddEventDynamicComponent } from './sportsManager/components/event/addeventdynamic/addeventdynamic.component';
import { AddEventManualComponent } from './sportsManager/components/event/addeventmanual/addeventmanual.component';
import { FixtureService } from './sportsManager/services/fixture.services';
import { LogReportComponent } from './sportsManager/components/logs/log-report.component';
import { LogService } from './sportsManager/services/log.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CustomMaterialModule } from './material.module';
import { CommonModule } from '@angular/common';


import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { EventsCalendar } from './sportsManager/components/event/eventGrid/event-calendar.component';

const appRoutes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'events', component: EventListComponent,
    canActivate: [AuthGuard],
    data: { onlyAdmin: false }
  },
  {
    path: 'addEvent', component: AddEventComponent,
    canActivate: [AuthGuard],
    data: { onlyAdmin: true }
  },
  {
    path: 'eventsCalendar', component: EventsCalendar,
    canActivate: [AuthGuard],
    data: { onlyAdmin: false }
  },
  {
    path: 'editTeam', component: EditTeamComponent,
    canActivate: [AuthGuard],
    data: { onlyAdmin: true }
  },
  {
    path: 'addUser', component: AddUserComponent,
    canActivate: [AuthGuard],
    data: { onlyAdmin: true }
  },
  {
    path: 'listUsers', component: ListUsersComponent,
    canActivate: [AuthGuard],
    data: { onlyAdmin: true }
  },
  {
    path: 'editUser', component: UserEditComponent,
    canActivate: [AuthGuard],
    data: { onlyAdmin: true }
  },
  {
    path: 'addSport', component: AddSportComponent,
    canActivate: [AuthGuard],
    data: { onlyAdmin: true }
  },
  {
    path: 'listSport', component: ListSportsComponent
    ,
    canActivate: [AuthGuard],
    data: { onlyAdmin: false }
  },
  {
    path: 'editsport', component: EditSportComponent,
    canActivate: [AuthGuard],
    data: { onlyAdmin: true }
  },
  {
    path: 'addTeam', component: AddTeamComponent,
    canActivate: [AuthGuard],
    data: { onlyAdmin: true }
  },
  {
    path: 'listTeams', component: ListTeamsComponent,
    canActivate: [AuthGuard],
    data: { onlyAdmin: true }
  },
  {
    path: 'logs', component: LogReportComponent,
    canActivate: [AuthGuard],
    data: { onlyAdmin: true }
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
    AddTeamComponent,
    ListTeamsComponent,
    EditTeamComponent,
    EventFormComponent,
    NavigationBar,
    AddEventComponent,
    AddEventDynamicComponent,
    AddEventManualComponent,
    LogReportComponent,
    EventsCalendar,
    PageNotFoundComponent
  ],
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: false } // <-- debugging purposes only
    ),
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory
    }),
    CommonModule,
    CustomMaterialModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  exports: [
    CustomMaterialModule,
    CalendarModule
  ],
  providers: [
    BaseService,
    SessionService,
    PermissionService,
    EventService,
    UserService,
    SportService,
    TeamService,
    FixtureService,
    LogService,
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