import { Component, OnInit, Input } from '@angular/core';
import { GameDataDto } from '../dtos/GameDataDto';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Input() gameData: GameDataDto;

  constructor() {
  }

  ngOnInit(): void {

  }
}
