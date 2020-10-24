import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { MemberRegistrationPageRoutingModule } from './member-registration-routing.module';

import { MemberRegistrationPage } from './member-registration.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    MemberRegistrationPageRoutingModule
  ],
  declarations: [MemberRegistrationPage]
})
export class MemberRegistrationPageModule {}
