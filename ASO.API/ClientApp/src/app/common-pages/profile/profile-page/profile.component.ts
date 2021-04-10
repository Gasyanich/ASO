import { Component, OnInit } from '@angular/core';
import { faUser, faAt, faPhone } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'aso-home-page',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  public userIcon = faUser;
  public atIcon = faAt;
  public phoneIcon = faPhone;

  public userData: {
    gender: string,
    email: string,
    phone: string
  } = {
    gender: 'Мужской',
    email: 'example@example.com',
    phone: '88005553535 проще позвонить, чем у кого-то занимать'
  };

  constructor() { }

  ngOnInit(): void {
  }

}
