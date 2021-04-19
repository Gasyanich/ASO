import {Component, OnInit} from '@angular/core';

import {SidebarItem} from './sidebar-item';
import {AuthService} from '../../auth/auth.service';
import {faHome, faUser, faMailBulk, faPlus, faUserGraduate, faUsers} from '@fortawesome/free-solid-svg-icons';
import {ProfileService} from 'src/app/common-pages/profile/profile.service';

@Component({
  selector: 'aso-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public homeIcon = faHome;
  public profileIcon = faUser;
  public chatIcon = faMailBulk;
  public regIcon = faPlus;
  public studentIcon = faUserGraduate;
  public employeeIcon = faUsers;

  public email = '...';
  public title = '...';
  public menuItems: SidebarItem[] = [];

  public isLoggedIn: boolean;

  constructor(
    private authService: AuthService,
    private profileService: ProfileService) {
    this.menuItems = [
      new SidebarItem('Главная', '/home', this.homeIcon),
      new SidebarItem('Профиль', '/profile', this.profileIcon),
      new SidebarItem('Чятик', '/chyat', this.chatIcon),
      new SidebarItem('Регистрация пользователя', '/register', this.regIcon),
      new SidebarItem('Обучающиеся', '/students', this.studentIcon),
      new SidebarItem('Сотрудники', '/employees', this.employeeIcon)
    ];

    this.isLoggedIn = this.authService.isLoggedIn();
    console.log(this.isLoggedIn);
  }

  ngOnInit(): void {
    this.profileService.getCurrentUser().subscribe(user => {
      this.email = user.email;
      this.title = user.role.displayName;
    });
  }

}
