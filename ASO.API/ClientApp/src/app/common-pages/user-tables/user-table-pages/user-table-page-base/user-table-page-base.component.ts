import {Component, OnInit} from '@angular/core';
import {UserService} from '../../user.service';
import {Role} from '../../../../core/models/users/role.model';
import {User} from '../../../../core/models/users/user.model';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatDialog} from '@angular/material/dialog';
import {ProfileDialogComponent} from '../../../profile/profile-dialog/profile-dialog.component';

@Component({
  selector: 'aso-user-table-page-base',
  templateUrl: './user-table-page-base.component.html',
  styleUrls: ['./user-table-page-base.component.scss']
})
export class UserTablePageBaseComponent implements OnInit {

  public users: User[];
  public isLoading = true;
  public searchText: string | undefined;

  public isShowRoleColumn: boolean;
  public pageTitle: string;
  protected roles: Role[];

  constructor(protected userService: UserService, protected snackBar: MatSnackBar, protected dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.userService.getUsersByRole(this.roles)
      .subscribe(
        (users: User[]) => {
          this.users = users;
          this.isLoading = false;
        },
      );
  }

  public deleteUser = (user: User): void => {
    this.userService.deleteUser(user)
      .subscribe(
        () => {
          this.users = this.users.filter(u => u.id !== user.id);
          this.snackBar.open('Пользователь успешно удалён', 'Ок');
        },
        (_: User) => this.snackBar.open('Возникла ошибка во время удаления пользователя', 'Ок')
      );
  }

  public showUserProfile = (user: User): void => {
    this.dialog.open(ProfileDialogComponent, {
      width: '600px',
      data: user,
      panelClass: 'aso-mat-dialog'
    });
  }
}
