import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService, UserData } from '../_services';
import { catchError } from 'rxjs/operators';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../_services/user.service';


@Component({
  selector: 'app-account',
  templateUrl: 'account.component.html',
  styleUrls: ['./account.component.css']
})

export class AccountComponent implements OnInit {

  dataForm: FormGroup;
  loading = true;
  submitted = false;
  returnUrl: string;
  error = '';

  passwordChange = false;

  user: UserData;
  isAdmin = false;


    constructor(
      private formBuilder: FormBuilder,
      private authenticationService: AuthenticationService,
      private toastr: ToastrService,
      private route: ActivatedRoute,
      private userService: UserService,
      private router: Router) {}

    ngOnInit() {

      this.isAdmin = JSON.parse(localStorage.getItem('isAdmin'));

      this.dataForm = this.formBuilder.group({
        username: [{value: '', disabled: true}, Validators.compose([Validators.required, Validators.minLength(3)])],
        email: ['', Validators.compose([Validators.required, Validators.email])],
        password: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
        cpassword: ['', Validators.compose([Validators.required, Validators.minLength(3)])]
      }, {
        validator: PasswordValidation.MatchPassword
      });

      this.route.queryParams
      .subscribe(params => {
        // console.log(params);
        const id = params.id;
        if (id && this.isAdmin) {
          this.userService.getUserById(id).subscribe(u => {
            this.setUser(u);
          });
        } else {

          this.authenticationService.getUserData().subscribe(u => {
            this.setUser(u);
          });
        }
      });
    }

    setUser(u: UserData) {
      this.user = u;
      this.dataForm.patchValue({
        username: u.userName,
        email: u.email
      });
      this.loading = false;
    }

    passwordCheck() {
      this.passwordChange = !this.passwordChange;
    }

    get f() { return this.dataForm.controls; }

    onSubmit() {
      this.submitted = true;

      // stop here if form is invalid
      if ( this.passwordChange  && this.dataForm.invalid) {
            return;
      } else if ( this.dataForm.get('username').invalid
            || this.dataForm.get('email').invalid) {
            return;
      }

        this.loading = true;
        if (!this.passwordChange) {
          this.authenticationService.updateEmail(this.user.id, this.f.email.value).pipe(catchError(err =>
            this.saveError(err)
          )).subscribe(a => this.saceSuccess());
        } else {
          this.authenticationService.updateEmailPassword(this.user.id, this.f.email.value, this.f.password.value).pipe(catchError(err =>
            this.saveError(err)
          )).subscribe(a => this.saceSuccess());
        }
    }

    saveError(err) {
      this.toastr.error('Changes could not be saved');
      this.loading = false;
      return err;
    }

    saceSuccess() {
      this.toastr.success('Saved changes');
      this.loading = false;
    }

    deleteUser(user: UserData) {
      this.userService.deleteUser(user.id).subscribe(u => {
        this.toastr.success('User deleted');
        this.router.navigate(['users']);
      });
    }

    grantAdmin(user: UserData) {
      this.userService.grantAdmin(user.id).subscribe(r => {
        this.toastr.success('Admin Granted');
      });
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
