import {Component, OnInit} from '@angular/core';
import {faUser, faAt, faPhone} from '@fortawesome/free-solid-svg-icons';
import {ProfileService} from '../profile.service';
import {User} from '../../../core/models/users/user.model';

@Component({
  selector: 'aso-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  public userIcon = faUser;
  public atIcon = faAt;
  public phoneIcon = faPhone;

  public user: User | undefined;

  public getUserFio(): string {
    return `${this.user?.lastName} ${this.user?.firstName} ${this.user?.patronymic}`;
  }

  constructor(private profileService: ProfileService) {
  }

  ngOnInit(): void {
    this.profileService.getCurrentUser()
      .subscribe(user => this.user = user);
  }

}
