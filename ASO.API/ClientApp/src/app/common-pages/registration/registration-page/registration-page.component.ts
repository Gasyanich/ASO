import {Component, OnInit} from '@angular/core';
import {RegistrationService} from '../registration.service';

import {ProfileService} from '../../profile/profile.service';
import {Role} from '../../../core/models/users/role.model';
import {MatSnackBar} from '@angular/material/snack-bar';
import {Sex, User} from '../../../core/models/users/user.model';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'aso-registration',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.scss']
})
export class RegistrationPageComponent implements OnInit {

  public user: User;

  // available roles for registration by current user
  public registrationRoles: Role[];

  constructor(private registrationService: RegistrationService,
              private profileService: ProfileService,
              private snackBar: MatSnackBar) {
    this.user = new User();
  }

  ngOnInit(): void {
    this.profileService.getCurrentUser()
      .subscribe(currentUser => {
        this.registrationRoles = this.registrationService.getRegistrationRoles(currentUser.role);
        this.initNewUser();
      });
  }

  public registerUser(form: NgForm): void {
    this.registrationService.registerUser(this.user)
      .subscribe(
        () => {
          this.snackBar.open('Пользователь успешно зарегестрирован', 'Ок');
          this.initNewUser();
          form.resetForm(this.user);
        },
        () => this.snackBar.open('Возникла ошибка во время регистрации пользователя', 'Ок')
      );
  }

  private initNewUser(): void {
    this.user = new User();
    this.user.sex = Sex.Male;
    this.user.role = this.registrationRoles[0];
  }
}
