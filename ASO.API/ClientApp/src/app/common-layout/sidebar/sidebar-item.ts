import {SidebarRouteData} from './sidebar-route-data';

export class SidebarItem {
  public path: string;
  public routeData: SidebarRouteData;

  constructor(path: string, routeData: SidebarRouteData) {
    this.path = path;
    this.routeData = routeData;
  }
}
