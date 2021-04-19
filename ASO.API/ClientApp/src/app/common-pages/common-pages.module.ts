import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { MaterialModule } from '../core/material/material.module';
import { AuthModule } from '../auth/auth.module';
import { ProfileComponent } from './profile/profile-page/profile.component';
import { AuthGuard } from '../auth/guards/auth.guard';
import { RegistrationPageComponent } from './registration/registration-page/registration-page.component';
import { CommonLayoutModule } from '../common-layout/common-layout.module';
import { PageTitleComponent } from './page-title/page-title.component';
import { AvatarPhotoComponent } from './profile/avatar-photo/avatar-photo.component';
import {FormsModule} from '@angular/forms';
import { UserTableComponent } from './user-table/user-table.component';

const routes: Routes = [
  {path: 'profile', component: ProfileComponent, canActivate: [AuthGuard]},
  {path: 'register', component: RegistrationPageComponent, canActivate: [AuthGuard]},
  {path: 'nothome', component: UserTableComponent, canActivate: [AuthGuard]}
];

@NgModule({
  declarations: [
    ProfileComponent,
    RegistrationPageComponent,
    PageTitleComponent,
    AvatarPhotoComponent,
    UserTableComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FontAwesomeModule,
    HttpClientModule,
    AuthModule,
    CommonLayoutModule,
    RouterModule.forRoot(routes),
    FormsModule,
  ],
  exports: [
    ProfileComponent,
    RouterModule
  ]
})
export class CommonPagesModule {
}
