import {Component} from '@angular/core';
import {UserService} from '../user.service';
import {UserTablePageBaseComponent} from './user-table-page-base/user-table-page-base.component';
import {Role} from '../../../core/models/users/role.model';
import {MANAGER, TEACHER} from '../../../core/models/constants/role.constants';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'aso-employee-table-page',
  templateUrl: './user-table-page-base/user-table-page-base.component.html',
  styleUrls: ['./user-table-page-base/user-table-page-base.component.scss']
})
export class EmployeeTablePageComponent extends UserTablePageBaseComponent {

  public pageTitle = 'Список сотрудников';
  public isShowRoleColumn = true;
  protected roles: Role[] = [MANAGER, TEACHER];

  constructor(protected userService: UserService, protected snackBar: MatSnackBar, protected dialog: MatDialog) {
    super(userService, snackBar, dialog);
  }


}
