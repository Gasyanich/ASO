import {IconDefinition} from '@fortawesome/fontawesome-common-types';
import {Role} from '../../core/models/users/role.model';

export class SidebarRouteData {
  public title: string;
  public icon: IconDefinition;
  public rolePermissions?: Role[];

  constructor(title: string, icon: IconDefinition, rolePermissions?: Role[]) {
    this.title = title;
    this.icon = icon;

    if (rolePermissions) {
      this.rolePermissions = rolePermissions;
    }
  }
}
