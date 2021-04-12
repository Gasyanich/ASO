import { Component, OnInit } from '@angular/core';

import { SidebarItem } from './sidebar-item';
import { AuthService } from '../../auth/auth.service';
import { faHome, faUser, faMailBulk, faPlus, faWheelchair } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'aso-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public homeIcon = faHome;
  public profileIcon = faUser;
  public notHomeIcon = faWheelchair;
  public chatIcon = faMailBulk;
  public regIcon = faPlus;

  // Получить по http с помощью какого-нибудь сервиса.
  public email = 'example@example.com';
  public title = 'Работяга';
  public menuItems: SidebarItem[] = [];

  public isLoggedIn: boolean;

  constructor(private authService: AuthService) {
    this.menuItems = [
      new SidebarItem('Главная', '/home', this.homeIcon),
      new SidebarItem('Профиль', '/profile', this.profileIcon),
      new SidebarItem('Не главная', '/nothome', this.notHomeIcon),
      new SidebarItem('Чятик', '/chyat', this.chatIcon),
      new SidebarItem('Регистрация пользователя', '/register', this.regIcon)
    ];

    this.isLoggedIn = true;// this.authService.isLoggedIn();
    console.log(this.isLoggedIn);
  }

  ngOnInit(): void {
  }

}
