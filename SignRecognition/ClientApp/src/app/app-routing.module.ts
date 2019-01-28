import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MainViewComponent } from './main-view/main-view.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { PredictionListComponent } from './prediction-list/prediction-list.component';


import { AuthGuard } from './_guards';
import { AccountComponent } from './account/account.component';
import { UserListComponent } from './user-list/user-list.component';

const routes: Routes = [
  { path: '', component: MainViewComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'predictionlist', component: PredictionListComponent, canActivate: [AuthGuard]},
  { path: 'account', component: AccountComponent, canActivate: [AuthGuard]},
  { path: 'users', component: UserListComponent, canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


