import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SignupPage } from './signup.page';

const routes: Routes = [
  {
    path: '',
    component: SignupPage
  },
  {
    path: 'driver',
    loadChildren: () => import('./driver-registration/driver-registration.module').then( m => m.DriverRegistrationPageModule)
  },
  {
    path: 'member',
    loadChildren: () => import('./member-registration/member-registration.module').then( m => m.MemberRegistrationPageModule)
  },
  {
    path: 'validate',
    loadChildren: () => import('./validate-registration/validate-registration.module').then( m => m.ValidateRegistrationPageModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SignupPageRoutingModule {}
