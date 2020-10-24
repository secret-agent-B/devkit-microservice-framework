import { Component, OnInit } from '@angular/core';
import { ImageService } from 'src/app/core/services/image.service';
import {
	ActionSheetController,
	Platform,
	ToastController,
} from '@ionic/angular';
import { DriverRegistration } from 'src/app/core/models/registration.model';
import { NavigationExtras, Router } from '@angular/router';

@Component({
	selector: 'app-driver-registration',
	templateUrl: './driver-registration.page.html',
	styleUrls: ['./driver-registration.page.scss'],
})
export class DriverRegistrationPage implements OnInit {
	userInformation = new DriverRegistration();

	isMobile = false;

	constructor(
		private imageService: ImageService,
		private actionSheetCtrl: ActionSheetController,
		private toastCtrl: ToastController ,
		private router : Router
	) {
		
		// DEV NOTE: Remove once the proc is completed
		this.userInformation.firstName = 'Ken';
		this.userInformation.middleName = 'Morales';
		this.userInformation.lastName = 'Adriano';
		this.userInformation.emailAddress = 'vhallaha@yahoo.com';
		this.userInformation.confirmEmailAddress = 'vhallaha@yahoo.com';
		this.userInformation.phoneNumber = '+13142107298';
		this.userInformation.address1 = '3002 Georgetown Farm Court';
		this.userInformation.city = 'Saint Ann';
		this.userInformation.province = 'MO';
		this.userInformation.country = 'USA';
		this.userInformation.zipCode = '63074';
		
	}

	ngOnInit() {}

	submitData() {
		let isValid = true; 
		
		// check for empty string 
		var properties = this.userInformation.TryNullValidation();
		if (properties === null) {
			isValid = false;
		}
		else if(properties.emailAddress !== properties.confirmEmailAddress){
			isValid = false;
		}

		if (!isValid) {
			this.toastCtrl
				.create({
					keyboardClose: true,
					color: 'danger',
					message:
						'All of the fields in this form are required please make sure to enter a valid information before you try to submit the form.',
					buttons: [
						{
							text: 'Ok',
							role: 'cancel',
						},
					],
				})
				.then((el) => el.present());
			return;
		}
		
		const navParams: NavigationExtras = {
			state: {
				userInfo: JSON.stringify(properties),
				userType: 'driver'
			}
		}
		this.router.navigate(['/signup/validate'], navParams);
	}

	//#region  Image Methods
	openCamera(file: HTMLInputElement, isForGov: boolean) {
		if (!this.isMobile) {
			file.click();
			return;
		}

		this.actionSheetCtrl
			.create({
				buttons: [
					{
						text: 'Use existing photo',
						handler: () => {
							this.imageService.chooseAPic();
						},
					},
					{
						text: 'Take a new photo',
						handler: () => {
							this.imageService.takeAPicture();
						},
					},
				],
			})
			.then((e) => {
				e.present();
			});
	}

	imageUpload(file: HTMLInputElement, isForGov: boolean) {
		this.imageService.getBase64(file.files[0]).subscribe((data) => { 
			if (isForGov) {
				this.userInformation.govt.photo = data;
			} else {
				this.userInformation.photo = data;
			}
		});
	}
	//#endregion Image Methods

}
