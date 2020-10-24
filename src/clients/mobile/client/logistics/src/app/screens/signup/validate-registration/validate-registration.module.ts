import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ValidateRegistrationPageRoutingModule } from './validate-registration-routing.module';

import { ValidateRegistrationPage } from './validate-registration.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ValidateRegistrationPageRoutingModule
  ],
  declarations: [ValidateRegistrationPage]
})
export class ValidateRegistrationPageModule {}
