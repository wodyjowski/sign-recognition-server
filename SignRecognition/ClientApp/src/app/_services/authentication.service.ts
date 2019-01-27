import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { EventEmitter, Output } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  @Output() getLoggedIn: EventEmitter<any> = new EventEmitter(true);
  @Output() getRegistered: EventEmitter<any> = new EventEmitter(true);

    constructor(private http: HttpClient) { }

    login(Username: string, Password: string) {
      return this.http.post<any>(`api/Login/Authenticate`, { Username, Password })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    this.getUser();
                    this.getLoggedIn.emit(true);
                }

                return user;
            }));
    }

    private getUser() {
        this.http.get<User>(`api/User`).subscribe(user => localStorage.setItem('currentUserName', JSON.stringify(user.userName)));
    }

    logout() {
        // remove user from local storage to log user out
        this.getLoggedIn.emit(false);
        localStorage.removeItem('currentUser');
        localStorage.removeItem('currentUserName');
    }

    register(Login: string, Email: string, Password: string) {
        return this.http.post<any>(`api/Login/Register`, { Login, Email, Password });
    }

    signalRegister() {
        this.getRegistered.emit(true);
    }

}

export interface User {
    userName: string;
}
