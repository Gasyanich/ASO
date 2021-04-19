import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {User} from '../../core/models/users/user.model';
import {Observable, of} from 'rxjs';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private user: User | undefined;

  constructor(private http: HttpClient) {
  }

  public getCurrentUser(): Observable<User> {
    if (this.user) {
      return of(this.user);
    }

    return this.http.get<User>('/me')
      .pipe(
        map((user: User) => {
          this.user = user;
          return user;
        })
      );
  }


}
