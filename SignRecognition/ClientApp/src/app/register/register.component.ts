import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError } from 'rxjs/operators';

import { AuthenticationService } from '../_services';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService) {}

    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            username: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
            email: ['', Validators.compose([Validators.required, Validators.email])],
            password: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
            cpassword: ['', Validators.compose([Validators.required, Validators.minLength(3)])]
        }, {
            validator: PasswordValidation.MatchPassword
          });

        const user = JSON.parse(localStorage.getItem('currentUser'));
        if (user) {
            this.router.navigate(['/']);
        }

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }

        this.loading = true;
        this.authenticationService.register(this.f.username.value, this.f.email.value, this.f.password.value).pipe(
            catchError(err => this.handleError(err)))
            .subscribe(
          Response => this.validateResponse(Response));
    }

    register() {
        this.router.navigate(['/register']);
    }

    handleError(err) {
            this.error = 'Fix input data';
            this.loading = false;
            window.console.log(err);
            window.console.log(err.error);
            return err;
    }

    validateResponse(Response: any) {
            if (Response.succeeded ) {
                    this.router.navigate(['login']);
                    this.authenticationService.signalRegister();
                } else {
                    window.console.log(Response);
                    this.loading = false;
                    this.error = Response.errors[0].description;
                }
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
