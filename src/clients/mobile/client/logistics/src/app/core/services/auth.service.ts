import { Injectable } from '@angular/core';
import { BehaviorSubject, from } from 'rxjs';
import { User, UserType } from '../models/user.model';
import { map, tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UserResponseData } from '../interfaces/UserApi.interface';

import { Plugins } from '@capacitor/core';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private _user = new BehaviorSubject<User>(null);

    constructor(private httpClient: HttpClient) {}

    get userIsAuthenticated() {
        return this._user.asObservable().pipe(
            map((user) => {
                return user != null;
            })
        );
    }

    get userId() {
        return this._user.asObservable().pipe(
            map((user) => {
                if (user) {
                    return user.id;
                } else {
                    return null;
                }
            })
        );
    }

    autoLogin() {
        return from(Plugins.Storage.get({ key: 'userData' })).pipe(
            // DEV NOTE: Check if the app has userdata in it and user that to
            //           automatically log the user into the app.
            map((storeData) => {
                if (!storeData || !storeData.value) {
                    return null;
                }

                const parseData = JSON.parse(
                    storeData.value
                ) as UserResponseData;
                const user = new User(
                    parseData.id,
                    parseData.firstName,
                    parseData.lastName,
                    parseData.emailAddress,
                    parseData.password,
                    parseData.type
                );

                return user;
            }),

            // DEV NOTE: set our observables to the users that we created.
            tap((user) => {
                if (user) {
                    this._user.next(user);
                }
            }),

            // DEV NOTE: make sure that this method still returns boolean
            map((user) => {
                return !!user;
            })
        );
    }

    signup(
        firstName: string,
        lastName: string,
        emailAddress: string,
        password: string,
        type: UserType
    ) {
        return this.httpClient
            .post(`${environment.apiBaseUrl}/users`, {
                firstName,
                lastName,
                emailAddress,
                password,
                type,
            })
            .pipe(tap(this.setUserData.bind(this)));
    }

    demoLogin(userData: UserResponseData){
        this.setUserData(userData);
    }

    // TODO : Change this to post instead of get.
    login(email: string, password: string) {
        return this.httpClient
            .get<UserResponseData>(
                `${environment.apiBaseUrl}/user?emailAddress=${email}&password=${password}`
            )
            .pipe(tap(this.setUserData.bind(this)));
    }

    logout() {
        this._user.next(null);
        Plugins.Storage.remove({ key: 'userData' });
    }

    private setUserData(userData: UserResponseData) {
        const user = new User(
            userData.id,
            userData.firstName,
            userData.lastName,
            userData.emailAddress,
            userData.password,
            userData.type
        );

        this._user.next(user);
        this.storeAuthData(
            user.id,
            user.firstName,
            user.lastName,
            user.emailAddress,
            user.type
        );
    }

    // TODO: Maybe put this into SQLite instead of storage.
    private storeAuthData(
        id: number,
        firstName: string,
        lastName: string,
        emailAddress: string,
        type: UserType
    ) {
        const data = { id, firstName, lastName, emailAddress, type };
        Plugins.Storage.set({ key: 'userData', value: JSON.stringify(data) });
    }
}
