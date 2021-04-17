import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'aso-avatar-photo',
  templateUrl: './avatar-photo.component.html',
  styleUrls: ['./avatar-photo.component.scss']
})
export class AvatarPhotoComponent implements OnInit {

  @Input() public photoUrl: string;
  @Input() public name: string;

  public initials = '';
  public showInitials = false;

  ngOnInit(): void {
    if (!this.photoUrl) {
      this.showInitials = true;
      this.createInitials();
    }
  }

  private createInitials(): void {
    let initials = '';

    for (let i = 0; i < this.name.length; i++) {
      if (this.name.charAt(i) === ' ') {
        continue;
      }

      if (this.name.charAt(i) === this.name.charAt(i).toUpperCase()) {
        initials += this.name.charAt(i);

        if (initials.length === 2) {
          break;
        }
      }
    }

    this.initials = initials;
  }

}
