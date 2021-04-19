import {Component, Input, OnInit} from '@angular/core';
import {Sex, User} from 'src/app/core/models/users/user.model';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'aso-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.scss']
})
export class UserTableComponent implements OnInit {

  @Input() public users: User[] = [];
  @Input() public isRoleColumnShown = false;

  @Input() set searchText(value: string) {
    console.log(value);
    if (value) {
      this.dataSource.filter = value.toLowerCase();
    }
    else {
      this.dataSource.filter = '';
    }
  }


  public dataSource: MatTableDataSource<User>;

  private displayedColumnsInternal: string[] = [
    'lastname',
    'name',
    'patronymic',
    'gender',
    'email',
    'phone'
  ];

  constructor() {
  }

  public get displayedColumns(): any {
    if (this.isRoleColumnShown) {
      return [...this.displayedColumnsInternal, 'role'];
    } else {
      return this.displayedColumnsInternal;
    }
  }


  ngOnInit(): void {
    if (this.isRoleColumnShown) {
      this.displayedColumns.push('role');
    }
    this.dataSource = new MatTableDataSource<User>(this.users);
  }

  public getUserSex(user: User): string {
    return user?.sex === Sex.Male ? 'М' : 'Ж';
  }
}
