import { Component, OnInit, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material/icon';
import { GameService } from '../services/GameService';
import { first } from 'rxjs/operators';
import { AuthService } from '../services/AuthService';
import { GameDataDto } from '../dtos/GameDataDto';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  @Input() gameData: GameDataDto;

  constructor(private gameService: GameService, private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer, private authService: AuthService) { 
    this.matIconRegistry.addSvgIcon(
      `profile_placeholder`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/images/profile_placeholder.svg")
    );
  }

  ngOnInit(): void {

  }

  onLogout() {
    console.log("onLogout()");
    this.authService.logout();
    location.reload(true);
  }

}
