import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CityFormComponent } from './city-form/city-form.component';
import { CityMainComponent } from './city-main/city-main.component';
import { AuthorizationGuard } from '../shared/autorizationGuard';

const routes: Routes = [
  { path: 'new', component: CityFormComponent },
  { 
    path: 'edit/:id', 
    component: CityFormComponent, 
    canActivate: [AuthorizationGuard], 
    data: {
       onlyAdmin: true 
      } 
  },
  // data: { roles: ['Admin'] }
  // { path: 'detail/:id', component: CityFormComponent },//add resolver
  { path: '', component: CityMainComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CityRoutingModule { }
