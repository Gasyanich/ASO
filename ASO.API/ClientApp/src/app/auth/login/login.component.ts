import {Component, OnInit} from '@angular/core';
import {AuthService} from '../auth.service';
import {UserLogin} from '../user-login.model';
import {UserLoginResult} from '../user-login-result.model';

@Component({
  selector: 'aso-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public userLogin: UserLogin;
  public userLoginResult: UserLoginResult | undefined;

  constructor(private authService: AuthService) {
    this.userLogin = new UserLogin();
  }

  ngOnInit(): void {
  }

  login(): void {
    this.authService.login(this.userLogin).subscribe(
      (loginResult: UserLoginResult) => {
        if (loginResult.isSuccess) {
          console.log('Логин успешный сучка');
        } else {
          this.userLoginResult = loginResult;
        }
      });
  }
}

