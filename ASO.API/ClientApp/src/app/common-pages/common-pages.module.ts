import {NgModule} from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';

import {MaterialModule} from '../core/material/material.module';
import {AuthModule} from '../auth/auth.module';
import {ProfilePageComponent} from './profile/profile-page/profile-page.component';
import {AuthGuard} from '../auth/guards/auth.guard';
import {RegistrationPageComponent} from './registration/registration-page/registration-page.component';
import {CommonLayoutModule} from '../common-layout/common-layout.module';
import {PageTitleComponent} from './page-title/page-title.component';
import {AvatarPhotoComponent} from './profile/avatar-photo/avatar-photo.component';
import {FormsModule} from '@angular/forms';
import {UserTableComponent} from './user-tables/user-table/user-table.component';
import {UserTablePageBaseComponent} from './user-tables/user-table-pages/user-table-page-base/user-table-page-base.component';
import {StudentTablePageComponent} from './user-tables/user-table-pages/student-table-page.component';
import {EmployeeTablePageComponent} from './user-tables/user-table-pages/employee-table-page.component';
import {ProfileComponent} from './profile/profile/profile.component';
import {ProfileDialogComponent} from './profile/profile-dialog/profile-dialog.component';


const routes: Routes = [
  {path: 'profile', component: ProfilePageComponent, canActivate: [AuthGuard]},
  {path: 'register', component: RegistrationPageComponent, canActivate: [AuthGuard]},
  {path: 'nothome', component: UserTableComponent, canActivate: [AuthGuard]},
  {path: 'students', component: StudentTablePageComponent, canActivate: [AuthGuard]},
  {path: 'employees', component: EmployeeTablePageComponent, canActivate: [AuthGuard]}
];

@NgModule({
  declarations: [
    ProfilePageComponent,
    RegistrationPageComponent,
    PageTitleComponent,
    AvatarPhotoComponent,
    UserTableComponent,
    UserTablePageBaseComponent,
    StudentTablePageComponent,
    EmployeeTablePageComponent,
    ProfileComponent,
    ProfileDialogComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FontAwesomeModule,
    HttpClientModule,
    AuthModule,
    CommonLayoutModule,
    RouterModule.forRoot(routes),
    FormsModule
  ],
  exports: [
    RouterModule
  ],
  entryComponents: [
    ProfileDialogComponent
  ],
})
export class CommonPagesModule {
}
