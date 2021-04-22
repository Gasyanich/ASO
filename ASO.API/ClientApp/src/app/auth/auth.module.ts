import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RouterModule , Routes} from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from '../core/material/material.module';
import { AuthService } from './auth.service';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
  {path: 'login', component: LoginComponent}
];

@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes),
    FlexLayoutModule,
    MaterialModule,
    FormsModule
  ],
  exports: [
    RouterModule
  ],
  providers: [AuthService]
})
export class AuthModule {
}
