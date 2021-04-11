import {NgModule} from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {CommonModule} from '@angular/common';

import {MaterialModule} from '../core/material/material.module';
import {SidebarComponent} from './sidebar/sidebar.component';
import {AuthModule} from '../auth/auth.module';
import { ContentCardComponent } from './content-card/content-card.component';


@NgModule({
  declarations: [
    SidebarComponent,
    ContentCardComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    HttpClientModule,
    AuthModule
  ],
  exports: [
    SidebarComponent,
    ContentCardComponent
  ]
})
export class CommonLayoutModule {
}
