import {Component, OnInit} from '@angular/core';
import {UserService} from '../../user.service';
import {Role} from '../../../../core/models/users/role.model';
import {User} from '../../../../core/models/users/user.model';

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

  constructor(private userService: UserService) {
  }

  ngOnInit(): void {
    this.userService.getUsersByRole(this.roles)
      .subscribe(
        (users: User[]) => {
          console.log(users);
          this.users = users;
          this.isLoading = false;
        },
      );
  }

}
