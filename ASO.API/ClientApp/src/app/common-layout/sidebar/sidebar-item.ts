export class SidebarItem {
  public name: string;
  public path?: string;
  private onClick?: () => {};

  constructor(name: string, path?: string, callback?: () => {}) {
    this.name = name;
    if (path) this.path = path;
    if (callback) this.onClick = callback;
  }
}
