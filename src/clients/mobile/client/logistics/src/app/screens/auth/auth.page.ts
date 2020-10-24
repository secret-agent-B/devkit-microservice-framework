import { Component, OnInit } from '@angular/core';
import { ToastController, LoadingController } from '@ionic/angular';
import { AuthService } from 'src/app/core/services/auth.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'app-auth',
    templateUrl: './auth.page.html',
    styleUrls: ['./auth.page.scss'],
})
export class AuthPage implements OnInit {
    constructor(
        private authService: AuthService,
        private toastCtrl: ToastController,
        private loadingCtrl: LoadingController,
        private router: Router
    ) {}

    ngOnInit() {}

    onSubmit(form: NgForm) {
        if (form.invalid) {
            return;
        }
         
        const email = form.value.email;
        const password = form.value.password;
 
        this.loadingCtrl
            .create({
                message: 'Logging in...',
            })
            .then((el) => {
                this.authService.login(email, password).subscribe(
                    (resp) => {
                        el.dismiss();
                        this.router.navigateByUrl('/Home');
                    },
                    (err) => {
                        el.dismiss();
                        this.toastCtrl
                            .create({
                                keyboardClose: true,
                                color: 'danger',
                                message:
                                    'Failed to login, please check your email & password',
                                buttons: [
                                    {
                                        text: 'Ok',
                                        role: 'cancel',
                                    },
                                ],
                            })
                            .then((el) => el.present());
                    }
                );

                el.present();
            });
    }
}
