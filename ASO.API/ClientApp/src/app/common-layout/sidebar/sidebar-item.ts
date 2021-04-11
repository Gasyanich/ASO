import { IconDefinition } from "@fortawesome/fontawesome-common-types";

export class SidebarItem {
  public name: string;
  public path?: string;
  public icon?: IconDefinition;
  private onClick?: () => {};

  constructor(name: string, path?: string, icon?: IconDefinition, callback?: () => {}) {
    this.name = name;
    if (path) this.path = path;
    if (callback) this.onClick = callback;
    if (icon) this.icon = icon;
  }
}
