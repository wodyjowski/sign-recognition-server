import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { EventEmitter, Output } from '@angular/core';

import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  @Output() getLoggedIn: EventEmitter<any> = new EventEmitter(true);
  @Output() getRegistered: EventEmitter<any> = new EventEmitter(true);

  private returnUrl: String;

    constructor(private http: HttpClient,
        private router: Router) { }

    user: User;


    login(Username: string, Password: string, returnUrl: string) {
        this.returnUrl = returnUrl;
        return this.http.post<any>(`api/Login/Authenticate`, { Username, Password })
                .pipe(map(user => {
                    // login successful if there's a jwt token in the response
                    if (user && user.token) {
                        // store user details and jwt token in local storage to keep user logged in between page refreshes
                        localStorage.setItem('currentUser', JSON.stringify(user));
                        this.getUserInfo();
                    }

                    return user;
                }));
    }

    private getUserInfo() {
        this.getUser().subscribe(user => this.emitLogin(user));
    }

    public getUser() {
        return this.http.get<User>(`api/User`);
    }

    public getUserData() {
        return this.http.get<UserData>(`api/User/Data`);
    }

    private emitLogin(user: User) {
        this.user = user;
        localStorage.setItem('currentUserName', JSON.stringify(user.userName));

        if (user.adminRights) {
            localStorage.setItem('isAdmin', JSON.stringify(user.adminRights));
        }

        this.getLoggedIn.emit(true);
        this.router.navigate([this.returnUrl]);
    }

    logout() {
        // remove user from local storage to log user out
        this.getLoggedIn.emit(false);
        localStorage.removeItem('currentUser');
        localStorage.removeItem('currentUserName');
        localStorage.removeItem('isAdmin');
    }

    register(Login: string, Email: string, Password: string) {
        return this.http.post<any>(`api/Login/Register`, { Login, Email, Password });
    }

    signalRegister() {
        this.getRegistered.emit(true);
    }

    updateEmail(userId: string, newEmail: string) {
        return this.http.post('api/User/UpdateEmail', { Id: userId, Email: newEmail } );
    }

    updateEmailPassword(userId: string, newEmail: string, newPassword: string) {
        return this.http.post('api/User/UpdateEmailPassword', { Id: userId, Email: newEmail, Password: newPassword } );
    }
}

interface User {
    userName: string;
    adminRights: boolean;
}

export interface UserData {
    id: string;
    userName: string;
    email: string;
    creationDate: Date;
    adminRights: boolean;
  }
