import { Component } from '@angular/core';
import { LoginResponseDto } from './dtos/LoginResponseDto';
import { Router } from '@angular/router';
import { AuthService } from './services/AuthService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  currentLogin: LoginResponseDto;

    constructor(
        private router: Router,
        private authService: AuthService
    ) {
        this.authService.currentLogin.subscribe(x => this.currentLogin = x);
    }

    logout() {
        this.authService.logout();
        this.router.navigate(['/login']);
    }
}
