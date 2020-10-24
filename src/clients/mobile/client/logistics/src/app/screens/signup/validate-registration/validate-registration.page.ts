import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {
	MemberRegistration,
	DriverRegistration,
} from 'src/app/core/models/registration.model';
import { GovInfo } from 'src/app/core/models/gov.info.model';
import { VehicleInfo } from 'src/app/core/models/vehicle.info.model';
import { LoadingController } from '@ionic/angular';
import { AuthService } from 'src/app/core/services/auth.service';
import { UserType } from 'src/app/core/models/user.model';

@Component({
	selector: 'app-validate-registration',
	templateUrl: './validate-registration.page.html',
	styleUrls: ['./validate-registration.page.scss'],
})
export class ValidateRegistrationPage implements OnInit {
	userType = '';
	userInformation: DriverRegistration;

	ngOnInit() {}
	constructor(
		private authServ: AuthService,
		private route: ActivatedRoute,
		private router: Router,
		private loadingCtrl: LoadingController
	) {
		this.userInformation = new DriverRegistration();
		this.route.queryParams.subscribe(() => {
			var params = this.router.getCurrentNavigation().extras.state;
			if (params === null || params === undefined) {
				this.router.navigateByUrl('/signup');
			}

			this.userType = params.userType;
			this.loadUserInformation(JSON.parse(params.userInfo));
		});
	}

	loadUserInformation(userInfo: DriverRegistration) {
		if (this.userType == 'member') {
			userInfo.govt = new GovInfo();
			userInfo.vehicle = new VehicleInfo();
		}

		this.userInformation = userInfo;
	}

	submitData() {
		this.loadingCtrl
			.create({
				message: 'Logging in...',
			})
			.then((el) => {
				setTimeout(() => {
					el.dismiss();

					let type: UserType;
					if(this.userType == 'driver'){
						type = UserType.Driver;
					} else {
						type = UserType.User;
					}

					this.authServ.demoLogin({
						id: 1,
						firstName: this.userInformation.firstName,
						lastName: this.userInformation.lastName,
						emailAddress: this.userInformation.emailAddress,
						password: '123',
						type: type
					});
					this.router.navigate(['/welcome']);
				}, 5000);

				el.present();
			});
	}
}
