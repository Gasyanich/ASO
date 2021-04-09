import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { MaterialModule } from '../core/material/material.module';
import { AuthModule } from '../auth/auth.module';
import { HomePageComponent } from './home-page/home-page.component';

const routes: Routes = [
  {path: 'home', component: HomePageComponent}
];

@NgModule({
  declarations: [
    HomePageComponent
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
    HomePageComponent,
    RouterModule
  ]
})
export class CommonPagesModule {
}
