import {Component, OnInit} from '@angular/core';
import {AuthService} from '../auth.service';
import {UserLogin} from '../user-login.model';
import {UserLoginResult} from '../user-login-result.model';
import {Router} from '@angular/router';

@Component({
  selector: 'aso-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public userLogin: UserLogin;
  public userLoginResult: UserLoginResult | undefined;

  constructor(private authService: AuthService, private router: Router) {
    this.userLogin = new UserLogin();
  }

  public ngOnInit(): void {
  }

  public login(): void {
    this.authService.login(this.userLogin).subscribe(
      (loginResult: UserLoginResult) => {
        this.userLoginResult = loginResult;

        if (loginResult.isSuccess) {
          this.router.navigateByUrl(this.authService.redirectUrl);
        }
      });
  }
}

