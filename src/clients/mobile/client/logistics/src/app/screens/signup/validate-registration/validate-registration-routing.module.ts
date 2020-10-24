import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ValidateRegistrationPage } from './validate-registration.page';

const routes: Routes = [
  {
    path: '',
    component: ValidateRegistrationPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ValidateRegistrationPageRoutingModule {}
