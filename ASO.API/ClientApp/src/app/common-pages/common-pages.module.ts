import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { MaterialModule } from '../core/material/material.module';
import { AuthModule } from '../auth/auth.module';
import { ProfileComponent } from './profile/profile-page/profile.component';
import {AuthGuard} from '../auth/guards/auth.guard';

const routes: Routes = [
  {path: 'profile', component: ProfileComponent, canActivate: [AuthGuard]}
];

@NgModule({
  declarations: [
    ProfileComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FontAwesomeModule,
    HttpClientModule,
    AuthModule,
    RouterModule.forRoot(routes),
  ],
  exports: [
    ProfileComponent,
    RouterModule
  ]
})
export class CommonPagesModule {
}
