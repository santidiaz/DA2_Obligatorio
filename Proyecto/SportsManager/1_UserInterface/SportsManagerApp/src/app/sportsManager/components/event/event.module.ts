/*import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CityRoutingModule } from './city-routing.module';

import { EventListComponent } from './eventList/eventList.component';

import { CityService } from '../service/city.service';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';

const eventRoutes: Routes = [
    { path: 'test', component: TestComponent },
    { path: 'event', component: EventListComponent },
    // { path: 'event/:id',      component: HeroDetailComponent },
    { path: '',
      redirectTo: '/login',
      pathMatch: 'full'
    },
    // { path: '**', component: PageNotFoundComponent }
  ];

@NgModule({
  imports: [
    CommonModule,
    CityRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [
    CityGridComponent,
    CityFormComponent,
    CityMainComponent,
    CityDetailComponent],
  providers: [
    CityService
  ],
  exports: [
    //CityMainComponent
  ]

})
export class CityModule { }
*/