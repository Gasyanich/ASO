import {Component, OnInit} from '@angular/core';
import {RegistrationService} from '../registration.service';
import {User} from '../../../core/models/users/user.model';
import {ProfileService} from '../../profile/profile.service';
import {Role} from '../../../core/models/users/role.model';
import {MatSnackBar} from '@angular/material/snack-bar';

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
        this.user.role = this.registrationRoles[0];
      });

    this.user.sex = 0;
  }

  public registerUser(): void {
    console.log(this.user);

    this.registrationService.registerUser(this.user)
      .subscribe(user => {
        console.log(user);
        this.snackBar.open('Пользователь успешно зарегестрирован');
      });
  }
}
