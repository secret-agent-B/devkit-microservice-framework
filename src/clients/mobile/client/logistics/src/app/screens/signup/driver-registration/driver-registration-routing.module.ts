import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DriverRegistrationPage } from './driver-registration.page';

const routes: Routes = [
  {
    path: '',
    component: DriverRegistrationPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DriverRegistrationPageRoutingModule {}
