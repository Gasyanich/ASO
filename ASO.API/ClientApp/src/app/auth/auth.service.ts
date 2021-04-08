import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpResponse} from '@angular/common/http';
import {Router} from '@angular/router';
import {UserLogin} from './user-login.model';
import {catchError, map} from 'rxjs/operators';
import {Observable, of} from 'rxjs';
import {UserLoginResult} from './user-login-result.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public redirectUrl = '';

  constructor(private http: HttpClient, private router: Router) {
  }

  public login(userLogin: UserLogin): Observable<UserLoginResult> {
    return this.http.post<string>(
      'https://localhost:5001/api/login',
      userLogin,
      {observe: 'response', responseType: 'text' as 'json'},
    ).pipe(
      map((response: HttpResponse<string>) => {
        localStorage.setItem('token', response.body as string);
        return new UserLoginResult(true, '');
      }),
      catchError((err: HttpErrorResponse) => {
        if (err.status === 401) {
          return of(new UserLoginResult(false, err.error));
        }

        // если вернулось не 401 при ошибке, то с серваком что-то не то
        return of(new UserLoginResult(false, 'Ошибка сервера. Повторите попытку позже'));
      })
    );
  }

  public logout(): void {
    // TODO: посылать запрос на бэкенд после того, как доделаем logout
    localStorage.removeItem('token');
    this.router.navigateByUrl('/login');
  }

  public isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !!token;
  }

}

