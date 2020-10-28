import { Component, OnInit } from '@angular/core';
import { UnitsService } from '../services/UnitsService';
import { first } from 'rxjs/operators';
import { UnitTypeDto } from '../dtos/UnitTypeDto';


@Component({
  selector: 'app-unit-type-window',
  templateUrl: './unit-type-window.component.html',
  styleUrls: ['./unit-type-window.component.css']
})
export class UnitTypeWindowComponent implements OnInit {

  unitTypes: UnitTypeDto[];

  constructor(private unitsService: UnitsService) { }

  ngOnInit(): void {
    this.unitsService.loadUnitTypes()
      .pipe(first())
      .subscribe(
        data => {
          this.unitTypes = data;
          console.log(data);
        },
        error => {
          console.log(error);
        }
      );
  }

}
