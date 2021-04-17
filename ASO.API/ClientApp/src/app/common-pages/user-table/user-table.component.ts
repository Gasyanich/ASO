import { Component, Input, OnInit } from '@angular/core';
import { Sex, User } from 'src/app/core/models/users/user.model';
import { ProfileService } from '../profile/profile.service';

@Component({
  selector: 'aso-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.scss']
})
export class UserTableComponent implements OnInit {

  public users: User[] = [];
  private isRoleColumnShown: boolean = false;

  private displayedColumnsInternal: string[] = [
    'lastname',
    'name',
    'patronymic',
    'gender',
    'email',
    'phone'
  ];

  public get displayedColumns() {
    if (this.isRoleColumnShown) {
      return [...this.displayedColumnsInternal, 'role'];
    } else {
      return this.displayedColumnsInternal;
    }
  }

  constructor(private profileService: ProfileService) { }

  ngOnInit(): void {
    if (this.isRoleColumnShown) this.displayedColumns.push('role');
    this.profileService.getCurrentUser().subscribe(user => {
      this.users = [user, user, user];
    });
  }

  public getUserSex(user: User) {
    return user?.sex === Sex.Male ? 'М' : 'Ж';
  }
}
