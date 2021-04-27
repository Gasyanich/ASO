import {AfterViewInit, Component, Input, OnInit, ViewChild} from '@angular/core';
import {User} from 'src/app/core/models/users/user.model';
import {MatTableDataSource} from '@angular/material/table';
import {MatSort} from '@angular/material/sort';

@Component({
  selector: 'aso-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.scss']
})
export class UserTableComponent implements OnInit, AfterViewInit {

  @Input() set users(users: User[]) {
    this.dataSource.data = users;
  }

  get users(): User[] {
    return this.dataSource.data;
  }

  @Input() public isRoleColumnShown = false;
  @Input() public deleteUser: (user: User) => void;
  @Input() public showUserProfile: (user: User) => void;
  @ViewChild(MatSort) public matSort: MatSort;

  @Input() set searchText(value: string) {
    if (value) {
      this.dataSource.filter = value.toLowerCase();
    } else {
      this.dataSource.filter = '';
    }
  }

  public dataSource = new MatTableDataSource<User>();

  private displayedColumnsInternal: string[] = [
    'lastName',
    'firstName',
    'patronymic',
    'email',
    'phoneNumber'
  ];

  constructor() {
  }

  public get displayedColumns(): any {
    if (this.isRoleColumnShown) {
      return [...this.displayedColumnsInternal, 'role', 'actions'];
    } else {
      return [...this.displayedColumnsInternal, 'actions'];
    }
  }

  ngOnInit(): void {
    if (this.isRoleColumnShown) {
      this.displayedColumns.push('role');
    }
    this.dataSource.data = this.users;
    this.dataSource.sortingDataAccessor = (user: User, sortHeaderId: string): string | number => {
      switch (sortHeaderId) {
        case 'role':
          return user.role.displayName;
        default:
          return user[sortHeaderId];
      }
    };
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.matSort;
  }

  public getRoleDisplayName(user: User): string {
    return user.role.displayName;
  }

}
