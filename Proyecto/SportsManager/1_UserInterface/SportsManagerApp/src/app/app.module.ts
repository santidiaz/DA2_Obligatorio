import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { LoginComponent } from './sportsManager/components/permissions/login.component';
import { EventListComponent } from './sportsManager/components/event/eventList/eventList.component';
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

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'event', component: EventListComponent },
  //{ path 'team', componet: MyTEmasComponetn },
  { path: 'addUser', component: AddUserComponent },
  { path: 'listUsers', component: ListUsersComponent },
  { path: 'editUser', component: UserEditComponent },
  { path: 'addSport', component: AddSportComponent },
  { path: 'listSport', component: ListSportsComponent },
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
    UserService,
    SportService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
