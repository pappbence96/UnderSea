import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { GameService } from '../services/GameService';
import { first } from 'rxjs/operators';
import { GameDataDto } from '../dtos/GameDataDto';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material/icon';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit, AfterViewInit, OnDestroy {

  gameData: GameDataDto;

  constructor(private gameService: GameService, private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer) {
    this.matIconRegistry.addSvgIcon(
      `profile_placeholder`,
      this.domSanitizer.bypassSecurityTrustResourceUrl('assets/images/profile_placeholder.svg')
    );
    this.matIconRegistry.addSvgIcon(
      `unit-1`,
      this.domSanitizer.bypassSecurityTrustResourceUrl('assets/images/units/1.svg')
    );
    this.matIconRegistry.addSvgIcon(
      `unit-2`,
      this.domSanitizer.bypassSecurityTrustResourceUrl('assets/images/units/2.svg')
    );
    this.matIconRegistry.addSvgIcon(
      `unit-3`,
      this.domSanitizer.bypassSecurityTrustResourceUrl('assets/images/units/3.svg')
    );
    this.matIconRegistry.addSvgIcon(
      `coral`,
      this.domSanitizer.bypassSecurityTrustResourceUrl('assets/images/resources/coral.svg')
    );
    this.matIconRegistry.addSvgIcon(
      `pearl`,
      this.domSanitizer.bypassSecurityTrustResourceUrl('assets/images/resources/pearl.svg')
    );
   }

  ngOnInit(): void {
    this.gameService.loadGameData()
      .pipe(first())
      .subscribe(
        data => {
          this.gameData = data;
        },
        error => {
          console.log(error);
        }
      );
  }

  ngAfterViewInit(): void {
    document.querySelector('.body').classList.add('authenticated');
    document.querySelector('.body').classList.remove('unauthenticated');
  }

  ngOnDestroy(): void {
    document.querySelector('.body').classList.remove('authenticated');
    document.querySelector('.body').classList.add('unauthenticated');
  }
}
