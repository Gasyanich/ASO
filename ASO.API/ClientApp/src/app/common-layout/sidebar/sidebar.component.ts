import {Component, OnInit} from '@angular/core';

import {SidebarItem} from './sidebar-item';
import {AuthService} from '../../auth/auth.service';

@Component({
  selector: 'aso-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  // Получить по http с помощью какого-нибудь сервиса.
  public email = 'example@example.com';
  public title = 'Работяга';
  public menuItems: SidebarItem[] = [];

  public isLoggedIn: boolean;

  constructor(private authService: AuthService) {
    this.menuItems = [
      new SidebarItem('Главная', '/home'),
      new SidebarItem('Профиль', '/profile'),
      new SidebarItem('Не главная', '/nothome'),
      new SidebarItem('Чятик', '/chyat'),
      new SidebarItem('Регистрация пользователя', '/register')
    ];

    this.isLoggedIn = this.authService.isLoggedIn();
    console.log(this.isLoggedIn);
  }

  ngOnInit(): void {
  }

}
