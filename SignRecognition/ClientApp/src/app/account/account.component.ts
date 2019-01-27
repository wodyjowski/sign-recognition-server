import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService, UserData } from '../_services';


@Component({
  selector: 'app-account',
  templateUrl: 'account.component.html',
  styleUrls: ['./account.component.css']
})

export class AccountComponent implements OnInit {

  registerForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';

  user: UserData;


    constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private authenticationService: AuthenticationService) {}

    ngOnInit() {
      this.registerForm = this.formBuilder.group({
        username: [{value: '', disabled: true}, Validators.compose([Validators.required, Validators.minLength(3)])],
        email: ['', Validators.compose([Validators.required, Validators.email])],
        password: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
        cpassword: ['', Validators.compose([Validators.required, Validators.minLength(3)])]
      }, {
        validator: PasswordValidation.MatchPassword
      });

      this.authenticationService.getUserData().subscribe(u => this.user = u);

    }

}

class PasswordValidation {
  static MatchPassword(AC: any) {
     const password: string = AC.get('password').value; // to get value in input tag
     const confirmPassword: string = AC.get('cpassword').value; // to get value in input tag
      if (password !== confirmPassword) {
          // window.console.log('false');
          AC.get('cpassword').setErrors( {MatchPassword: true} );
      } else {
          // window.console.log('true');
          return null;
      }
  }
}
