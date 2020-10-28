import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../services/AuthService';
import { GameDataDto } from '../dtos/GameDataDto';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  @Input() gameData: GameDataDto;

  constructor(private authService: AuthService) { 
  }

  ngOnInit(): void {

  }

  onLogout() {
    console.log("onLogout()");
    this.authService.logout();
    location.reload(true);
  }

}
