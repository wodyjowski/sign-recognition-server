import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MainViewComponent } from './main-view/main-view.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { LoclistComponent } from './loclist/loclist.component';

import { ImageListComponent } from './image-list/image-list.component';
import { ImageUploadComponent } from './image-upload/image-upload.component';

import { AuthGuard } from './_guards';

const routes: Routes = [
  { path: '', component: MainViewComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'locationlist', component: LoclistComponent, canActivate: [AuthGuard]},
  { path: 'photos', component: ImageListComponent, canActivate: [AuthGuard]},
  { path: 'upload', component: ImageUploadComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


