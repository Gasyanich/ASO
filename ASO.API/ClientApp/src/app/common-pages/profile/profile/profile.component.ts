import {Component, Input, OnInit} from '@angular/core';
import {faAt, faPhone, faUser} from '@fortawesome/free-solid-svg-icons';
import {Sex, User} from '../../../core/models/users/user.model';

@Component({
  selector: 'aso-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public userIcon = faUser;
  public atIcon = faAt;
  public phoneIcon = faPhone;

  @Input() public user: User | undefined;

  constructor() {
  }

  ngOnInit(): void {
  }

  public getUserFio(): string {
    return `${this.user?.lastName} ${this.user?.firstName} ${this.user?.patronymic}`;
  }

  public getUserSex(): string {
    return this.user?.sex === Sex.Male ? 'Мужской' : 'Женский';
  }

}
