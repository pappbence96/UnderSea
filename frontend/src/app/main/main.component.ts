import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { GameService } from '../services/GameService';
import { first } from 'rxjs/operators';
import { GameDataDto } from '../dtos/GameDataDto';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit, AfterViewInit, OnDestroy {

  gameData: GameDataDto;

  constructor(private gameService: GameService) { }

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

  ngAfterViewInit() {
    console.log("Main afterviewinit");
    let body = document.querySelector('.body');
    console.log(body);
    console.log(body.classList);
    body.classList.add('authenticated');
    document.querySelector('.body').classList.remove('unauthenticated');
  }
  
  ngOnDestroy() {
    console.log("Main ondestroy");
    document.querySelector('.body').classList.remove('authenticated');
    document.querySelector('.body').classList.add('unauthenticated');
  }
}
