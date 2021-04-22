import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest} from '@angular/common/http';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {

  private apiUrl = 'https://localhost:5001/api';

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const reqClone = req.clone(
      {url: this.apiUrl + req.url}
    );

    return next.handle(reqClone);


  }
}
