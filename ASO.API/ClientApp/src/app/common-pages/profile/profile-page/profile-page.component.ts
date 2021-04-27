import {Component, OnInit} from '@angular/core';
import {ProfileService} from '../profile.service';
import {User} from '../../../core/models/users/user.model';

@Component({
  selector: 'aso-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent implements OnInit {

  public user: User | undefined;


  constructor(private profileService: ProfileService) {
  }

  ngOnInit(): void {
    this.profileService.getCurrentUser()
      .subscribe(user => this.user = user);
  }

}
