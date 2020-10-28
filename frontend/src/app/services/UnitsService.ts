import { Injectable } from '@angular/core';
import { UnitTypeDto } from '../dtos/UnitTypeDto';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class UnitsService {
    private typesSubject: BehaviorSubject<UnitTypeDto[]>;
    public types: Observable<UnitTypeDto[]>;

    constructor(private http: HttpClient) {
        this.typesSubject = new BehaviorSubject<UnitTypeDto[]>([]);
        this.types = this.typesSubject.asObservable();
    }

    loadUnitTypes() {
        return this.http
            .get<UnitTypeDto[]>("http://localhost:51554/api/units/types")
            .pipe(map(data => {
                this.typesSubject.next(data);
                return data;
            }));
    }
}