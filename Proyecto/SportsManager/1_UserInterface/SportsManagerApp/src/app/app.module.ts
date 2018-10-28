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
import { SessionService } from './sportsManager/services/session.service'
import { PermissionService } from './sportsManager/services/permission.service'
import { BaseService } from './sportsManager/services/baseService'

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'event', component: EventListComponent },
  //{ path: 'event/:id',      component: HeroDetailComponent },
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
    PermissionService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
