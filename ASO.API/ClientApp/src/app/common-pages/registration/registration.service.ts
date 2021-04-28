import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {User} from '../../core/models/users/user.model';
import {Role} from '../../core/models/users/role.model';
import {Observable} from 'rxjs';
import {DIRECTOR, MANAGER, STUDENT, TEACHER} from '../../core/models/constants/role.constants';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(private http: HttpClient) {
  }

  public registerUser(user: User): Observable<User> {
    const urlApi = this.getRegisterUrlApiByRole(user.role);

    return this.http.post<User>(urlApi, user);
  }

  public getRegistrationRoles(currentRole: Role): Role[] {
    switch (currentRole.name) {
      case MANAGER.name:
        return [STUDENT];
      case DIRECTOR.name:
        return [STUDENT, TEACHER, MANAGER];
      default:
        return [];
    }
  }

  private getRegisterUrlApiByRole(role: Role): string {
    return '/users?role=' + role.name;
  }

}
