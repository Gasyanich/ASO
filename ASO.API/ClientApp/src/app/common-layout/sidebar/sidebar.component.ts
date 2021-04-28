import {Component, OnInit} from '@angular/core';

import {ProfileService} from 'src/app/common-pages/profile/profile.service';
import {SidebarItem} from './sidebar-item';
import {COMMON_PAGE_ROUTES} from '../../common-pages/common-pages.routes';

@Component({
  selector: 'aso-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  public email = '...';
  public title = '...';
  public menuItems: SidebarItem[] = [];

  public isLoggedIn: boolean;

  constructor(private profileService: ProfileService) {
  }

  ngOnInit(): void {
    this.profileService.getCurrentUser().subscribe(user => {
      this.email = user.email;
      this.title = user.role.displayName;

      this.menuItems = this.getSidebarItems();
    });
  }

  private getSidebarItems(): SidebarItem[] {
    const currentUserRole = this.profileService.user.role.name.toUpperCase();

    return COMMON_PAGE_ROUTES
      .filter(route => {
        const rolePermissions = route.data.routeData.rolePermissions;
        return !rolePermissions || rolePermissions.some(role => role.name === currentUserRole);
      })
      .map(route => new SidebarItem(`/${route.path}`, route.data.routeData));
  }

}
