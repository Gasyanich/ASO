import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'aso-edit-rules',
  templateUrl: './edit-rules.component.html',
  styleUrls: ['./edit-rules.component.scss']
})
export class EditRulesComponent implements OnInit {
  public rulesContent;

  constructor() { }

  ngOnInit(): void {
    this.initContent();
  }

  public initContent() {
    this.rulesContent = "";//TODO initialize from server
  }

  public saveContent() {
    console.log("Content > " + this.rulesContent);
    //saveToBackend(this.rulesContent);
  }
}
