import { Component, OnInit } from '@angular/core';

import { SidebarItem } from './sidebar-item';

@Component({
  selector: 'aso-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  // Получить по http с помощью какого-нибудь сервиса.
  public email: string = 'example@example.com';
  public title: string = 'Работяга';
  public menuItems: SidebarItem[] = [];

  constructor() {
    this.menuItems = [
      new SidebarItem('Главная'),
      new SidebarItem('Профиль'),
      new SidebarItem('Не главная'),
      new SidebarItem('Чятик'),
      new SidebarItem('Регистрация пользователя')
    ];
   }

  ngOnInit(): void {
  }

}
