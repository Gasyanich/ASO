import {Component} from '@angular/core';
import {UserTablePageBaseComponent} from './user-table-page-base/user-table-page-base.component';
import {STUDENT} from '../../../core/models/constants/role.constants';
import {Role} from '../../../core/models/users/role.model';
import {UserService} from '../user.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'aso-student-table-page',
  templateUrl: './user-table-page-base/user-table-page-base.component.html',
  styleUrls: ['./user-table-page-base/user-table-page-base.component.scss']
})
export class StudentTablePageComponent extends UserTablePageBaseComponent {

  public pageTitle = 'Список обучающихся';
  public isShowRoleColumn = false;
  protected roles: Role[] = [STUDENT];

  constructor(protected userService: UserService, protected snackBar: MatSnackBar, protected dialog: MatDialog) {
    super(userService, snackBar, dialog);
  }

}
