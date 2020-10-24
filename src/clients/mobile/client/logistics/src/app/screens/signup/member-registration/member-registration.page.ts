import { Component, OnInit } from '@angular/core';
import { ImageService } from 'src/app/core/services/image.service';
import { ActionSheetController, ToastController } from '@ionic/angular';
import { MemberRegistration } from 'src/app/core/models/registration.model';
import { Router, NavigationExtras } from '@angular/router';

@Component({
	selector: 'app-member-registration',
	templateUrl: './member-registration.page.html',
	styleUrls: ['./member-registration.page.scss'],
})
export class MemberRegistrationPage implements OnInit {
	userInformation = new MemberRegistration();

	isMobile = false;

	constructor(
		private imageService: ImageService,
		private actionSheetCtrl: ActionSheetController,
		private toastCtrl: ToastController,
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
				userType: 'member'
			}
		}
		this.router.navigate(['/signup/validate'], navParams);
	}

	//#region Image Methods
	openCamera(file: HTMLInputElement) {
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

	imageUpload(file: HTMLInputElement) {
		this.imageService.getBase64(file.files[0]).subscribe((data) => {
			this.userInformation.photo = data;
		});
	}
	//#endregion Image Methods

}
