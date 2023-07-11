import {Component} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  selectedLink: 'Employees' | 'Managers' | 'Tasks' = 'Employees';

  constructor() {
  }

  changeTitle(title: 'Employees' | 'Managers' | 'Tasks') {
    this.selectedLink = title;
  }
}
