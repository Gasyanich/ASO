import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Role} from '../../core/models/users/role.model';
import {Observable} from 'rxjs';
import {User} from '../../core/models/users/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {
  }

  public getUsersByRole(roles: Role[]): Observable<User[]> {
    const roleNames = roles.map(role => role.name).join(',');

    return this.http.get<User[]>(`/Users?roles=${roleNames}`);
  }

  public deleteUser(user: User): Observable<any> {
    return this.http.delete(`/Users?role=${user.role.name}&id=${user.id}`);
  }

}
