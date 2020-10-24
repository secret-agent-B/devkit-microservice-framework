import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MemberRegistrationPage } from './member-registration.page';

const routes: Routes = [
  {
    path: '',
    component: MemberRegistrationPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MemberRegistrationPageRoutingModule {}
