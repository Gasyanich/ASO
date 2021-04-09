import { Component, OnInit } from '@angular/core';
import { faUser, faAt, faPhone } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'aso-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
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
  }

  constructor() { }

  ngOnInit(): void {
  }

}
