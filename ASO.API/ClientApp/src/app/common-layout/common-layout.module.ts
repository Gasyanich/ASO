import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { MaterialModule } from '../core/material/material.module';
import { SidebarComponent } from './sidebar/sidebar.component';
import { AuthModule } from '../auth/auth.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';


@NgModule({
  declarations: [
    SidebarComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    HttpClientModule,
    FontAwesomeModule,
    AuthModule
  ],
  exports: [
    SidebarComponent
  ]
})
export class CommonLayoutModule {
}
