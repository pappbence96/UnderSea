import { Component, OnInit, Input } from '@angular/core';
import { GameService } from '../services/GameService';
import { first } from 'rxjs/operators';
import { GameDataDto } from '../dtos/GameDataDto';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Input() gameData: GameDataDto;

  constructor(private gameService: GameService, private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer) { 
    this.matIconRegistry.addSvgIcon(
      `unit-1`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/images/units/1.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `unit-2`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/images/units/2.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `unit-3`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/images/units/3.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `coral`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/images/resources/coral.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `pearl`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/images/resources/pearl.svg")
    );
  }

  ngOnInit(): void {
    
  }
}
