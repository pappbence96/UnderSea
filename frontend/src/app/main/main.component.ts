import { Component, OnInit } from '@angular/core';
import { GameService } from '../services/GameService';
import { first } from 'rxjs/operators';
import { GameDataDto } from '../dtos/GameDataDto';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

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
}
