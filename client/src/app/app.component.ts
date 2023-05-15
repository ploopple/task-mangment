import { Component } from '@angular/core';
import { AuthService } from './guards/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  constructor(private authService: AuthService) {}
  ngOnInit() {
      this.authService.getUserInfo()
  }
}
