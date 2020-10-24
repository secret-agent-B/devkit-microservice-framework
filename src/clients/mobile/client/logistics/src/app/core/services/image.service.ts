import { Injectable } from '@angular/core';
import { ToastController, Platform } from '@ionic/angular';

import { Camera, CameraOptions } from '@ionic-native/camera/ngx';
import { Chooser } from '@ionic-native/chooser/ngx';

import { from, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
	providedIn: 'root',
})
export class ImageService {
	options: CameraOptions = {
		quality: 100,
		destinationType: this.camera.DestinationType.FILE_URI,
		encodingType: this.camera.EncodingType.PNG,
		mediaType: this.camera.MediaType.PICTURE,
	};

	constructor(
		private camera: Camera,
		private platform: Platform,
		private toastCtrl: ToastController,
		private choose: Chooser
	) {}

	takeAPicture() {
		return from(this.camera.getPicture(this.options)).pipe(
			map((imageData) => {
				if (imageData == null) {
					this.showError();
					return null;
				}

				return `data:image/png;base64,${imageData}`;
			})
		);
	}

	chooseAPic() {
		return from(this.choose.getFile('image/png')).pipe(
			map((imageData) => {
				if (imageData == null) {
					this.showError();
					return;
				}

				debugger;
			})
		);
	}

	getBase64(file) {
		const subject = new Subject<any>();
		const imgReader = new FileReader();
		imgReader.onload = () => {
			subject.next(imgReader.result);
		};

		imgReader.readAsDataURL(file);

		return subject;
	}

	private showError() {
		this.toastCtrl
			.create({
				keyboardClose: true,
				color: 'danger',
				message:
					'Failed to take a picture, please make sure that you allow us to access your camera.',
				buttons: [
					{
						text: 'Ok',
						role: 'cancel',
					},
				],
			})
			.then((el) => el.present());
	}
}
