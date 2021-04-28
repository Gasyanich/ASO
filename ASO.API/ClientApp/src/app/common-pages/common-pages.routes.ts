import {Routes} from '@angular/router';
import {ProfilePageComponent} from './profile/profile-page/profile-page.component';
import {RegistrationPageComponent} from './registration/registration-page/registration-page.component';
import {StudentTablePageComponent} from './user-tables/user-table-pages/student-table-page.component';
import {EmployeeTablePageComponent} from './user-tables/user-table-pages/employee-table-page.component';
import {DIRECTOR, MANAGER} from '../core/models/constants/role.constants';
import {faPlus, faUser, faUserGraduate, faUsers} from '@fortawesome/free-solid-svg-icons';
import {SidebarRouteData} from '../common-layout/sidebar/sidebar-route-data';

const regIcon = faPlus;
const studentIcon = faUserGraduate;
const profileIcon = faUser;
const employeeIcon = faUsers;

export const COMMON_PAGE_ROUTES: Routes = [
  {
    path: 'profile',
    component: ProfilePageComponent,
    data: {
      routeData: new SidebarRouteData('Профиль', profileIcon)
    }
  },
  {
    path: 'register',
    component: RegistrationPageComponent,
    data: {
      routeData: new SidebarRouteData('Регистрация пользователя', regIcon, [MANAGER, DIRECTOR])
    }
  },
  {
    path: 'students',
    component: StudentTablePageComponent,
    data: {
      routeData: new SidebarRouteData('Обучающиеся', studentIcon, [MANAGER, DIRECTOR])
    }
  },
  {
    path: 'employees',
    component: EmployeeTablePageComponent,
    data: {
      routeData: new SidebarRouteData('Сотрудники', employeeIcon, [DIRECTOR])
    }
  }
];
