import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { DriverRegistrationPageRoutingModule } from './driver-registration-routing.module';

import { DriverRegistrationPage } from './driver-registration.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    DriverRegistrationPageRoutingModule
  ],
  declarations: [DriverRegistrationPage]
})
export class DriverRegistrationPageModule {}
