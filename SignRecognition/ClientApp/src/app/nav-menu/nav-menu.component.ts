import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services';

import {Router} from '@angular/router';

import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  isLoggedTittle = 'Login';
  status = true;

  userName = null;

  infoMsg = '';

  constructor(private authenticationService: AuthenticationService,
    private toastr: ToastrService,
    private router: Router) {
    authenticationService.getLoggedIn.subscribe(logged => this.changeTitle(logged));
    authenticationService.getRegistered.subscribe(registered => this.notifyRegister(registered));
  }

  ngOnInit() {
    const user = JSON.parse(localStorage.getItem('currentUser'));
    if (user) {
      this.isLoggedTittle = 'Logout';
      this.status = false;
      this.getCurrenUserName();
      // window.console.log(user);
    }
  }

  private changeTitle(logged: boolean): void {
    if (logged) {
      this.isLoggedTittle = 'Logout';
      this.status = false;
      this.toastr.success('Logged in');
      this.getCurrenUserName();
    } else {
      if (!this.status) {
        this.isLoggedTittle = 'Login';
        this.status = true;
        this.toastr.info('Logged out');
      }
    }
  }

  loginClick() {
    if (!this.status) {
      // reset login status
      this.authenticationService.logout();
      this.router.navigate(['/']);
    } else {
      this.router.navigate(['/login']);
    }
  }

  notifyRegister(registered) {
    this.toastr.success('Successfully registered');
  }

  private getCurrenUserName() {
    this.userName = JSON.parse(localStorage.getItem('currentUserName'));
  }

}
