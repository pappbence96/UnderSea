import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../services/AuthService';
import { GameDataDto } from '../dtos/GameDataDto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  @Input() gameData: GameDataDto;

  constructor(private authService: AuthService, private router: Router) {
  }

  ngOnInit(): void {

  }

  onLogout(): void {
    this.authService.logout();
    location.reload();
  }

  onUnitsClick(): void {
    this.router.navigate(['units']);
  }

}
